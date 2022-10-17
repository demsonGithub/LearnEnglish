﻿using Dapper;
using Demkin.Transcoding.Domain;
using Demkin.Transcoding.Domain.Interfaces;
using Microsoft.Data.SqlClient;

namespace Demkin.Transcoding.WebApi.Services
{
    public class TranscodeBgService : BackgroundService
    {
        private readonly IServiceScope _serviceScope;

        private string _connectionString = string.Empty;
        private readonly ITranscodeFileRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _env;
        private readonly ITranscodeService _transcodeService;
        private readonly IConfiguration _configuration;

        public TranscodeBgService(IServiceScopeFactory serviceFactory)
        {
            _serviceScope = serviceFactory.CreateScope();
            var sp = _serviceScope.ServiceProvider;
            _httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
            _env = sp.GetRequiredService<IWebHostEnvironment>(); ;
            _transcodeService = sp.GetRequiredService<ITranscodeService>(); ;
            _configuration = sp.GetRequiredService<IConfiguration>();
            _repository = sp.GetRequiredService<ITranscodeFileRepository>();

            _connectionString = _configuration.GetSection("DbConnection:MasterDb_Transcode").Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var connection = new SqlConnection(_connectionString);

            while (!stoppingToken.IsCancellationRequested)
            {
                // 1. 从数据库中获取需要转码的任务
                string sql = @"SELECT * FROM TranscodeFile WHERE TranscodeStatus=@transcodeStatus";

                var transcodeFiles = await connection.QueryAsync<TranscodeFile>(sql, new { transcodeStatus = TranscodeStatus.Ready });

                // 2. 循环遍历转码
                foreach (var item in transcodeFiles)
                {
                    try
                    {
                        // 转码消耗资源，因此串行转码
                        await TranscodeProcessAsync(item);
                    }
                    catch (Exception ex)
                    {
                        item.Fail(ex.Message);
                        await _repository.UpdateAsync(item);
                        await _repository.UnitOfWork.SaveEntitiesAsync();
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(3));
            }
        }

        private async Task TranscodeProcessAsync(TranscodeFile transcodeFile)
        {
            transcodeFile.Start();
            await _repository.UpdateAsync(transcodeFile);
            await _repository.UnitOfWork.SaveEntitiesAsync();

            // 1.得到源文件，将原视频下载保存临时文件 ,否则无权限操作解码
            var fileInfo = await DownloadAsync(transcodeFile.SourceUrl);

            // 2. 创建一个转码后保存的文件路径，确保不存在
            string stroageFolderPath = Path.Combine(_env.WebRootPath, Constant.TempFolder);
            if (!Directory.Exists(stroageFolderPath))
                Directory.CreateDirectory(stroageFolderPath);

            // 获取文件名称，无后缀
            string targetFileNameNoExt = Path.GetFileNameWithoutExtension(transcodeFile.SourceUrl);
            string targetFolderPath = Path.GetDirectoryName(transcodeFile.SourceUrl);
            string targetFolderName = Path.GetFileName(targetFolderPath);

            DateTime today = DateTime.Today;
            string folderKey = $"{today.Year}/{today.Month}/{today.Day}/{targetFolderName}";

            string targetFolder = Path.Combine(stroageFolderPath, folderKey);
            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);

            string targetFilePath = Path.Combine(targetFolder, $"{targetFileNameNoExt}.{transcodeFile.TargetFormat}");

            // 3. 使用ffmpeg工具转码
            try
            {
                await _transcodeService.TranscodeFileToTarget(fileInfo.FullName, targetFilePath);
            }
            catch (Exception)
            {
            }
            finally
            {
                fileInfo.Delete();
            }

            // 4. 转码完成上传到存储，然后返回地址 todo 上传
            var transcodeUrl = await _transcodeService.UploadFile("http://localhost:8082/api/Upload/UploadFile", targetFilePath);

            // 5. 完成转码，触发领域事件
            transcodeFile.Complete(transcodeUrl);
            await _repository.UpdateAsync(transcodeFile);
            await _repository.UnitOfWork.SaveEntitiesAsync();
        }

        private async Task<FileInfo> DownloadAsync(string sourceUrl)
        {
            // 创建临时文件夹目录
            string tempDir = Path.Combine(Path.GetTempPath(), Constant.TempFolder);
            // 源文件的临时保存路径
            string sourceFullPath = Path.Combine(tempDir, Guid.NewGuid() + Path.GetExtension(sourceUrl));

            FileInfo fileInfo = new FileInfo(sourceFullPath);
            fileInfo.Directory?.Create();

            // 开始下载文件
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var responseMsg = await httpClient.GetAsync(sourceUrl);
            if (!responseMsg.IsSuccessStatusCode)
            {
                throw new ArgumentException($"下载源文件失败, {responseMsg.StatusCode}", nameof(responseMsg));
            }
            using (FileStream fs = new FileStream(sourceFullPath, FileMode.Create))
                await responseMsg.Content.CopyToAsync(fs);

            return fileInfo;
        }
    }
}