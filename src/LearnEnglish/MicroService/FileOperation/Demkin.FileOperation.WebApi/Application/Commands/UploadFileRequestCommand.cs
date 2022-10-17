using Demkin.Utils.IdGenerate;
using DotNetCore.CAP;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System.Diagnostics;

namespace Demkin.FileOperation.WebApi.Application.Commands
{
    public class UploadFileRequestCommand : IRequest<UploadFileInfo>
    {
        /// <summary>
        /// SignalR的标识Id
        /// </summary>
        public string? IdentityId { get; set; }

        public IFormFile File { get; set; }
    }

    public class UploadFileRequestCommandHandler : IRequestHandler<UploadFileRequestCommand, UploadFileInfo>
    {
        private readonly IMemoryCache _cache;
        private readonly IConnectionMultiplexer _redisCoon;
        private readonly FileDomainService _domainService;
        private readonly ICapPublisher _capPublisher;
        private readonly IUploadFileInfoRepository _uploadFileInfoRepository;

        public UploadFileRequestCommandHandler(IMemoryCache cache,
            IConnectionMultiplexer redisCoon,
            FileDomainService domainService, ICapPublisher capPublisher, IUploadFileInfoRepository uploadFileInfoRepository)
        {
            _cache = cache;
            _redisCoon = redisCoon;
            _domainService = domainService;
            _capPublisher = capPublisher;
            _uploadFileInfoRepository = uploadFileInfoRepository;
        }

        public async Task<UploadFileInfo> Handle(UploadFileRequestCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;
            string fileName = file.FileName;
            using Stream stream = file.OpenReadStream();

            string cacheKey = "FileOperation." + IdGenerateHelper.Instance.GenerateId();
            if (!string.IsNullOrEmpty(request.IdentityId))
            {
                await _capPublisher.PublishAsync("FileOperation.UploadFile.Progress", new { IdentityId = request.IdentityId, RedisCacheKey = cacheKey });
            }
            var redisDb = _redisCoon.GetDatabase();
            // 上传文件
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    int completedPercent = _cache.Get<int>(cacheKey);
                    if (!string.IsNullOrEmpty(request.IdentityId))
                    {
                        await redisDb.StringSetAsync(cacheKey, completedPercent);
                    }
                    Thread.Sleep(100);
                    if (completedPercent >= 100)
                    {
                        _cache.Remove(cacheKey);
                        break;
                    }
                }
            });
            var result = await _domainService.UploadFileAsync(fileName, stream, cacheKey, cancellationToken);

            if (result.isOldData)
            {
                return result.uploadFileInfo;
            }

            // 添加到数据库
            var uploadFileInfoEntity = await _uploadFileInfoRepository.AddAsync(result.uploadFileInfo, cancellationToken);

            bool isSaveSuccess = await _uploadFileInfoRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (!isSaveSuccess)
                throw new DomainException("保存到数据库失败");

            return uploadFileInfoEntity;
        }
    }
}