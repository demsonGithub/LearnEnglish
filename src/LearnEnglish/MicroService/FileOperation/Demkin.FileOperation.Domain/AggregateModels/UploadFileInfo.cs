namespace Demkin.FileOperation.Domain.AggregateModels
{
    public class UploadFileInfo : Entity<long>, IAggregateRoot
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSizeBytes { get; private set; }

        /// <summary>
        /// 散列值，文件大小和散列值一样，基本就是同一个文件
        /// </summary>
        public string FileSHA256Hash { get; private set; }

        /// <summary>
        /// 网络访问地址
        /// </summary>
        public Uri RemoteUrl { get; private set; }

        /// <summary>
        /// 备份文件地址
        /// </summary>
        public Uri BackupUrl { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; private set; }

        private UploadFileInfo()
        { }

        public static UploadFileInfo Create(string fileName, long fileSizeBytes, string fileSHA256Hash, Uri remoteUrl, Uri backupUrl)
        {
            UploadFileInfo item = new UploadFileInfo()
            {
                Id = IdGenerateHelper.Instance.GenerateId(),
                FileName = fileName,
                FileSizeBytes = fileSizeBytes,
                FileSHA256Hash = fileSHA256Hash,
                RemoteUrl = remoteUrl,
                BackupUrl = backupUrl,
                CreateTime = DateTime.Now
            };
            item.AddDomainEvent(new UploadFileDomainEvent(item));
            return item;
        }
    }
}