using Demkin.Transcoding.Domain;
using Demkin.Transcoding.WebApi.Extensions;
using Demkin.Transcoding.WebApi.Models;
using DotNetCore.CAP;
using Newtonsoft.Json;

namespace Demkin.Transcoding.WebApi.Application.IntegrationEvents
{
    public class TranscodeFileIntegrationEvent : ICapSubscribe
    {
        private readonly ICapPublisher _capPublisher;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _env;
        private readonly TranscodeService _transcodeService;

        public TranscodeFileIntegrationEvent(ICapPublisher capPublisher,
            IHttpClientFactory httpClientFactory, IWebHostEnvironment env,
            TranscodeService transcodeService)
        {
            _capPublisher = capPublisher;
            _httpClientFactory = httpClientFactory;
            _env = env;
            _transcodeService = transcodeService;
        }

        [CapSubscribe("Transcoding.Audio")]
        public async Task HandleTransodeFile(object parameters)
        {
            //将json字符转换为实体对象
            TranscodeFileInputParams? inputParams = JsonConvert.DeserializeObject<TranscodeFileInputParams>(Convert.ToString(parameters));

            await _capPublisher.PublishAsync("Transcoding.Audio.Result", new { RedisKey = inputParams.RedisKey, Status = "Started", HasTranscodeFileUrl = "" });

            // 1. 下载视频，否则无权限操作解码 创建临时文件夹
            string tempDir = Path.Combine(Path.GetTempPath(), "SF");
            // 源文件的临时保存路径
            string sourceFullPath = Path.Combine(tempDir, Guid.NewGuid() + Path.GetExtension(inputParams.MediaUrl));
            FileInfo sourceFile = new FileInfo(sourceFullPath);
            sourceFile.Directory?.Create();

            HttpClient httpClient = _httpClientFactory.CreateClient();
            var statusCode = await httpClient.DownloadFileAsync(new Uri(inputParams.MediaUrl), sourceFullPath);

            if (statusCode != System.Net.HttpStatusCode.OK)
            {
                sourceFile.Delete();
                throw new Exception("下载失败");
            }

            string filePath = Path.Combine(_env.ContentRootPath, "wwwroot", "TranscodeFile");
            if (!Directory.Exists(filePath))
                new DirectoryInfo(filePath).Create();

            // 先获取文件夹路径
            string folderPath = Path.GetDirectoryName(inputParams.MediaUrl);
            // 获取文件夹名称
            string folderName = Path.GetFileName(folderPath);
            // 获取文件的名称并且不要后缀
            string fileNameNoExt = Path.GetFileNameWithoutExtension(inputParams.MediaUrl);

            // 确保目标文件路径不存在
            DateTime today = DateTime.Today;

            //用日期把文件分散在不同文件夹存储，同时由于加上了文件hash值作为目录，又用用户上传的文件夹做文件名，
            //所以几乎不会发生不同文件冲突的可能
            //用用户上传的文件名保存文件名，这样用户查看、下载文件的时候，文件名更灵活
            string fileKey = $"{today.Year}/{today.Month}/{today.Day}/{folderName}";
            string targetFolder = Path.Combine(filePath, fileKey);
            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);

            string targetUrl = Path.Combine(targetFolder, $"{fileNameNoExt}.m4a");

            await _transcodeService.TranscodeFileToM4a(sourceFile.FullName, targetUrl);

            // 转码完成，反馈结果
            await _capPublisher.PublishAsync("Transcoding.Audio.Result",
                new
                {
                    RedisKey = inputParams.RedisKey,
                    Status = "Completed",
                    HasTranscodeFileUrl = targetUrl
                });
        }
    }
}