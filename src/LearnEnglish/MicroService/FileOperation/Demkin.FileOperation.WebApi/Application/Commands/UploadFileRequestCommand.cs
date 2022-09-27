using Demkin.FileOperation.WebApi.Application.IntegrationEvents;
using DotNetCore.CAP;

namespace Demkin.FileOperation.WebApi.Application.Commands
{
    public class UploadFileRequestCommand : IRequest<UploadFileInfo>
    {
        public IFormFile File { get; set; }
    }

    public class UploadFileRequestCommandHandler : IRequestHandler<UploadFileRequestCommand, UploadFileInfo>
    {
        private readonly FileDomainService _domainService;
        private readonly ICapPublisher _capPublisher;
        private readonly IUploadFileInfoRepository _uploadFileInfoRepository;

        public UploadFileRequestCommandHandler(FileDomainService domainService, ICapPublisher capPublisher, IUploadFileInfoRepository uploadFileInfoRepository)
        {
            _domainService = domainService;
            _capPublisher = capPublisher;
            _uploadFileInfoRepository = uploadFileInfoRepository;
        }

        public async Task<UploadFileInfo> Handle(UploadFileRequestCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;
            string fileName = file.FileName;
            using Stream stream = file.OpenReadStream();

            // 上传文件
            var result = await _domainService.UploadFileAsync(fileName, stream, cancellationToken);

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