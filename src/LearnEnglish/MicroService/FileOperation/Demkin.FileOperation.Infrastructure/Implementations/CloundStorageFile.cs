using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Demkin.FileOperation.Infrastructure.Implementations
{
    /// <summary>
    /// 存放公网，这个类实现存储在wwwroot下
    /// </summary>
    public class CloundStorageFile : IStorageFile
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StorageType StorageType => StorageType.Public;

        public CloundStorageFile(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Uri> SaveFileAsync(string key, Stream content, CancellationToken cancellationToken = default)
        {
            if (key.StartsWith("/"))
            {
                throw new DomainException($"key不能以'/'开头,{nameof(key)}");
            }
            content.Position = 0;
            StreamReader streamReader = new StreamReader(content);
            Console.WriteLine(streamReader.ReadToEnd());

            string webDir = Path.Combine(_env.ContentRootPath, "wwwroot");
            string fullPath = Path.Combine(webDir, key);
            // 判断路径的目录是否存在
            string fullPathDir = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(fullPathDir))
            {
                Directory.CreateDirectory(fullPathDir);
            }
            // 如果存在，尝试删除
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                content.Position = 0;
                await content.CopyToAsync(stream);
            }

            var request = _httpContextAccessor.HttpContext.Request;
            string url = request.Scheme + "://" + request.Host + "/FileService/" + key;
            return new Uri(url);
        }
    }
}