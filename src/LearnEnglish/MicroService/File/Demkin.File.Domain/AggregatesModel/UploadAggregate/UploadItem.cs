using Demkin.File.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.File.Domain.AggregatesModel.UploadAggregate
{
    public class UploadItem : Entity<long>, IAggregateRoot
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
        /// 备份文件地址
        /// </summary>
        public string BackupUrl { get; private set; }

        /// <summary>
        /// 网络访问地址
        /// </summary>
        public string RemoteUrl { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; private set; }

        private UploadItem()
        { }

        public UploadItem(string fileName, long fileSizeBytes, string fileSHA256Hash, string backupUrl, string remoteUrl)
        {
            FileName = fileName;
            FileSizeBytes = fileSizeBytes;
            FileSHA256Hash = fileSHA256Hash;
            BackupUrl = backupUrl;
            RemoteUrl = remoteUrl;
            CreateTime = DateTime.Now;

            AddDomainEvent(new UploadFileDomainEvent(this));
        }
    }
}