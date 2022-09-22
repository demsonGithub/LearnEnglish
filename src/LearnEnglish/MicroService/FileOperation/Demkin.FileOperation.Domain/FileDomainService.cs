namespace Demkin.FileOperation.Domain
{
    public class FileDomainService : IDenpendencyScope
    {
        private readonly IUploadFileInfoRepository _uploadFileInfoRepository;
        private readonly IStorageFile _storageFile;

        public FileDomainService(IUploadFileInfoRepository uploadFileInfoRepository, IStorageFile storageFile)
        {
            _uploadFileInfoRepository = uploadFileInfoRepository;
            _storageFile = storageFile;
        }

        /// <summary>
        /// 判断数据库中是否存在文件
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public async Task<UploadFileInfo> FindFileAsync(Stream stream)
        {
            // 创建文件的hash值
            var hash = HashHelper.ComputeSha256Hash(stream);
            long fileSize = stream.Length;

            // 判断数据库中是否存在文件
            var oldFileInfo = await _uploadFileInfoRepository.FindFileAsync(fileSize, hash);
            return oldFileInfo;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="stream"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UploadFileInfo> UploadFileAsync(string fileName, Stream stream, CancellationToken cancellationToken)
        {
            // 创建文件的hash值
            var hash = HashHelper.ComputeSha256Hash(stream);
            long fileSize = stream.Length;

            // 判断文件是否存在
            var oldFileInfo = await _uploadFileInfoRepository.FindFileAsync(fileSize, hash);
            if (oldFileInfo != null)
                throw new DomainException("已存在相同的文件");

            DateTime today = DateTime.Today;

            //用日期把文件分散在不同文件夹存储，同时由于加上了文件hash值作为目录，又用用户上传的文件夹做文件名，
            //所以几乎不会发生不同文件冲突的可能
            //用用户上传的文件名保存文件名，这样用户查看、下载文件的时候，文件名更灵活
            string key = $"{today.Year}/{today.Month}/{today.Day}/{hash}/{fileName}";

            // 上传文件,领域层不操作数据库所以返回实体对象，由应用层添加保存数据库
            Uri remoteUrl = await _storageFile.SaveFileAsync(key, stream, cancellationToken);
            stream.Position = 0;

            UploadFileInfo uploadFileInfo = new UploadFileInfo(fileName, fileSize, hash, remoteUrl, null);

            return uploadFileInfo;
        }
    }
}