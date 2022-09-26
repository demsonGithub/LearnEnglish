namespace Demkin.FileOperation.WebApi.Application.Commands
{
    public class UploadFileRequestCommand : IRequest<UploadFileInfo>
    {
        public IFormFile File { get; set; }
    }

    public class UploadFileRequestCommandHandler : IRequestHandler<UploadFileRequestCommand, UploadFileInfo>
    {
        private readonly FileDomainService _domainService;
        private readonly IUploadFileInfoRepository _uploadFileInfoRepository;

        public UploadFileRequestCommandHandler(FileDomainService domainService, IUploadFileInfoRepository uploadFileInfoRepository)
        {
            _domainService = domainService;
            _uploadFileInfoRepository = uploadFileInfoRepository;
        }

        public async Task<UploadFileInfo> Handle(UploadFileRequestCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;
            string fileName = file.FileName;
            using Stream stream = file.OpenReadStream();

            var isExistFileInfo = await _domainService.FindFileAsync(stream);
            if (isExistFileInfo != null)
            {
                return isExistFileInfo;
            }
            // 上传文件
            var uploadfileInfo = await _domainService.UploadFileAsync(fileName, stream, cancellationToken);

            // 添加到数据库
            var result = await _uploadFileInfoRepository.AddAsync(uploadfileInfo, cancellationToken);

            bool isSaveSuccess = await _uploadFileInfoRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (!isSaveSuccess)
            {
                throw new DomainException("保存到数据库失败");
            }

            return result;
        }
    }
}