using Demkin.FileOperation.Domain.Interfaces;
using Demkin.Utils;
using MediatR;

namespace Demkin.FileOperation.WebApi.Application.Commands
{
    public class UploadFileRequestCommand : IRequest<string>
    {
        public IFormFile File { get; set; }
    }

    public class UploadFileRequestCommandHandler : IRequestHandler<UploadFileRequestCommand, string>
    {
        private readonly IStorageFile _storageFile;

        public UploadFileRequestCommandHandler(IStorageFile storageFile)
        {
            _storageFile = storageFile;
        }

        public async Task<string> Handle(UploadFileRequestCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;

            string fileName = file.FileName;

            string url = "";
            using Stream stream = file.OpenReadStream();
            string hash = HashHelper.ComputeSha256Hash(stream);
            DateTime today = DateTime.Today;
            //用日期把文件分散在不同文件夹存储，同时由于加上了文件hash值作为目录，又用用户上传的文件夹做文件名，
            //所以几乎不会发生不同文件冲突的可能
            //用用户上传的文件名保存文件名，这样用户查看、下载文件的时候，文件名更灵活
            string key = $"{today.Year}/{today.Month}/{today.Day}/{hash}/{fileName}";

            url = (await _storageFile.SaveFileAsync(key, stream, cancellationToken)).ToString();
            return url;
        }
    }
}