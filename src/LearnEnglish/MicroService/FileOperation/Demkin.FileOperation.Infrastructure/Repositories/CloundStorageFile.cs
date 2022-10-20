using Demkin.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Demkin.FileOperation.Infrastructure.Repositories
{
    /// <summary>
    /// 存放公网，这个类实现存储在wwwroot下
    /// </summary>
    public class CloundStorageFile : IStorageFile
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<CloundStorageFile> _logger;
        private readonly IMemoryCache _cache;
        private int _completedPercent = 0;

        public StorageType StorageType => StorageType.Public;

        public CloundStorageFile(IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor, ILogger<CloundStorageFile> logger, IMemoryCache cache)
        {
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<string> SaveFileAsync(string fileName, Stream content, string cacheKey, CancellationToken cancellationToken = default)
        {
            if (fileName.StartsWith("/"))
            {
                throw new DomainException($"key不能以'/'开头,{nameof(fileName)}");
            }
            string webDir = Path.Combine(_env.ContentRootPath, "wwwroot", "UploadedResources");

            DateTime today = DateTime.Today;
            //用日期把文件分散在不同文件夹存储，同时由于加上了文件hash值作为目录，又用用户上传的文件夹做文件名，
            //所以几乎不会发生不同文件冲突的可能
            //用用户上传的文件名保存文件名，这样用户查看、下载文件的时候，文件名更灵活
            var hash = HashHelper.ComputeSha256Hash(content);

            string folder = $"{today.Year}/{today.Month}/{today.Day}/{hash}";

            // 判断文件夹是否存在
            string storageFolder = Path.Combine(webDir, folder);
            if (!Directory.Exists(storageFolder))
            {
                Directory.CreateDirectory(storageFolder);
            }

            string storageFilePath = Path.Combine(storageFolder, fileName);
            try
            {
                // 如果存在，尝试删除
                if (File.Exists(storageFilePath))
                {
                    File.Delete(storageFilePath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"上传文件:{ex.Message}");
            }

            using (var fsWrite = new FileStream(storageFilePath, FileMode.Create))
            {
                _completedPercent = 0;
                byte[] buffer = new byte[1024 * 1024 * 1];
                int readByteCount = 0;
                content.Seek(0, SeekOrigin.Begin);

                while (true)
                {
                    readByteCount = await content.ReadAsync(buffer, 0, buffer.Length);

                    await fsWrite.WriteAsync(buffer, 0, readByteCount);
                    _completedPercent = Convert.ToInt32(fsWrite.Length / Convert.ToDouble(content.Length) * 100);

                    if (!string.IsNullOrEmpty(cacheKey))
                        _cache.Set(cacheKey, _completedPercent);
                    if (readByteCount == 0)
                        break;
                }
            }

            string url = $"/UploadedResources/{folder}/{fileName}";

            return url;
        }

        public int GetCompletedPercent() => _completedPercent;
    }
}