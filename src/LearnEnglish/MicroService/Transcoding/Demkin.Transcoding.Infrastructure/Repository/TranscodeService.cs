using Demkin.Core;
using Demkin.Transcoding.Domain.Interfaces;
using FFmpeg.NET;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Demkin.Transcoding.Infrastructure.Repository
{
    public class FFMpegTranscodeService : ITranscodeService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FFMpegTranscodeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task TranscodeFileToTarget(string sourceUrl, string targetUrl, CancellationToken ct = default)
        {
            var inputFile = new InputFile(sourceUrl);
            var outputFile = new OutputFile(targetUrl);

            string baseDir = AppContext.BaseDirectory;
            string ffmpegPath = Path.Combine(baseDir, "FFmpeg", "ffmpeg.exe");
            var ffmpeg = new Engine(ffmpegPath);

            string? errorMsg = null;
            ffmpeg.Error += (sender, e) =>
            {
                errorMsg += e.Exception.Message;
            };

            await ffmpeg.ConvertAsync(inputFile, outputFile, ct);

            if (!string.IsNullOrEmpty(errorMsg))
            {
                throw new Exception(errorMsg);
            }
        }

        public async Task<string> UploadFile(string uploadApiUrl, string filePath)
        {
            // 1. 实例化HttpClient
            HttpClient client = _httpClientFactory.CreateClient();
            // 2. 读取文件
            Stream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            string fileName = Path.GetFileName(filePath);
            try
            {
                // 4. 实例化Multipart表单模型
                MultipartFormDataContent formData = new MultipartFormDataContent();
                // 设定文本类型表单项，使用StringContent
                formData.Add(new StringContent(""), "IdentityId");
                // 设定文件类型表单项, 使用StreamContent
                formData.Add(new StreamContent(fileStream), "File", fileName);

                // 5. 发送请求
                var response = await client.PostAsync(uploadApiUrl, formData);
                // 6. 接收结果
                string result = response.Content.ReadAsStringAsync().Result;

                var resultInfo = JsonConvert.DeserializeObject<ApiResult<ResponseMsg>>(result);

                return resultInfo.data.RemoteUrl?.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                fileStream.Dispose();
                client.Dispose();
            }
        }
    }

    public class ResponseMsg
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 网络访问地址
        /// </summary>
        public Uri RemoteUrl { get; set; }
    }
}