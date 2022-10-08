using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace Demkin.FileOperation.Infrastructure.Repositories
{
    /// <summary>
    /// 存放公网，这个类实现存储在wwwroot下
    /// </summary>
    public class CloundStorageFile : IStorageFile
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StorageType StorageType => StorageType.Public;

        public CloundStorageFile(IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor)
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
            string webDir = Path.Combine(_env.ContentRootPath, "wwwroot", "UploadedResources");
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
                CancellationTokenSource cts = new CancellationTokenSource();

                Thread td = new Thread(() =>
                {
                    try
                    {
                        while (true)
                        {
                            double vl = content.Position / content.Length;
                            Console.WriteLine($"已复制:{vl}%");

                            Thread.Sleep(1);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });
                td.Start();
                await content.CopyToAsync(stream);
                td.Interrupt();
            }

            var request = _httpContextAccessor.HttpContext.Request;
            string url = request.Scheme + "://" + request.Host + "/UploadedResources/" + key;
            return new Uri(url);
        }
    }
}