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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CloundStorageFile> _logger;
        private readonly IMemoryCache _cache;
        private int _completedPercent = 0;

        public StorageType StorageType => StorageType.Public;

        public CloundStorageFile(IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor, ILogger<CloundStorageFile> logger, IMemoryCache cache)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _cache = cache;
        }

        public async Task<Uri> SaveFileAsync(string cacheKey, string key, Stream content, CancellationToken cancellationToken = default)
        {
            if (key.StartsWith("/"))
            {
                throw new DomainException($"key不能以'/'开头,{nameof(key)}");
            }
            string webDir = Path.Combine(_env.ContentRootPath, "wwwroot", "UploadedResources");
            string fullPath = Path.Combine(webDir, key);
            // 判断路径的目录是否存在
            string fullPathDir = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(fullPathDir))
            {
                Directory.CreateDirectory(fullPathDir);
            }
            try
            {
                // 如果存在，尝试删除
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"上传文件:{ex.Message}");
            }

            using (var fsWrite = new FileStream(fullPath, FileMode.Create))
            {
                _completedPercent = 0;
                byte[] buffer = new byte[1024 * 1024 * 1];
                int readByteCount = 0;

                while (true)
                {
                    readByteCount = await content.ReadAsync(buffer, 0, buffer.Length);
                    await fsWrite.WriteAsync(buffer, 0, readByteCount);
                    _completedPercent = Convert.ToInt32(fsWrite.Length / Convert.ToDouble(content.Length) * 100);

                    _cache.Set(cacheKey, _completedPercent);
                    if (readByteCount == 0)
                        break;
                }
            }

            var request = _httpContextAccessor.HttpContext.Request;
            string url = request.Scheme + "://" + request.Host + "/UploadedResources/" + key;
            return new Uri(url);
        }

        public int GetCompletedPercent() => _completedPercent;
    }
}