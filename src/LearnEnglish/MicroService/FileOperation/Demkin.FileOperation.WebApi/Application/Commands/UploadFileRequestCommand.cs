using Demkin.FileOperation.WebApi.Application.IntegrationEvents;
using Demkin.FileOperation.WebApi.Hubs;
using DotNetCore.CAP;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace Demkin.FileOperation.WebApi.Application.Commands
{
    public class UploadFileRequestCommand : IRequest<UploadFileInfo>
    {
        public IFormFile File { get; set; }
    }

    public class UploadFileRequestCommandHandler : IRequestHandler<UploadFileRequestCommand, UploadFileInfo>
    {
        private readonly IMemoryCache _cache;
        private readonly FileDomainService _domainService;
        private readonly IHubContext<FileUploadStatusHub> _hubContext;
        private readonly ICapPublisher _capPublisher;
        private readonly IUploadFileInfoRepository _uploadFileInfoRepository;

        private volatile bool isStopProgress = false;

        public UploadFileRequestCommandHandler(IMemoryCache cache, FileDomainService domainService, IHubContext<FileUploadStatusHub> hubContext, ICapPublisher capPublisher, IUploadFileInfoRepository uploadFileInfoRepository)
        {
            _cache = cache;
            _domainService = domainService;
            _hubContext = hubContext;
            _capPublisher = capPublisher;
            _uploadFileInfoRepository = uploadFileInfoRepository;
        }

        public async Task<UploadFileInfo> Handle(UploadFileRequestCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;
            string fileName = file.FileName;
            using Stream stream = file.OpenReadStream();

            var hash = HashHelper.ComputeSha256Hash(stream);
            DateTime today = DateTime.Today;
            string key = $"{today.Year}/{today.Month}/{today.Day}/{hash}/{fileName}";

            // 上传文件
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    int completedPercent = _cache.Get<int>(key);

                    _hubContext.Clients.All.SendAsync("RecieveMessage", completedPercent);
                    Thread.Sleep(100);
                    if (completedPercent >= 100 || isStopProgress)
                    {
                        _cache.Remove(key);
                        break;
                    }
                }
            });
            var result = await _domainService.UploadFileAsync(fileName, stream, cancellationToken);
            isStopProgress = true;

            if (result.isOldData)
            {
                await _capPublisher.PublishAsync("UploadFileCompleted", new UploadFileCompletedIntegrationEvent(result.uploadFileInfo.Id));
                return result.uploadFileInfo;
            }

            // 添加到数据库
            var uploadFileInfoEntity = await _uploadFileInfoRepository.AddAsync(result.uploadFileInfo, cancellationToken);

            bool isSaveSuccess = await _uploadFileInfoRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (!isSaveSuccess)
                throw new DomainException("保存到数据库失败");

            // 完成上传，通知订阅者
            await _capPublisher.PublishAsync("UploadFileCompleted", new UploadFileCompletedIntegrationEvent(uploadFileInfoEntity.Id));

            return uploadFileInfoEntity;
        }
    }
}