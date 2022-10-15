using Demkin.Domain.Abstraction;
using Demkin.Transcoding.Domain.Events;
using Demkin.Utils.IdGenerate;

namespace Demkin.Transcoding.Domain
{
    public class TranscodeFile : Entity<long>, IAggregateRoot
    {
        private TranscodeFile()
        {
        }

        #region

        /// <summary>
        /// 文件标题
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 源文件路径
        /// </summary>
        public string SourceUrl { get; private set; }

        /// <summary>
        /// 文件的散列值
        /// </summary>
        public string? FileSHA256Hash { get; private set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long? FileSizeBytes { get; private set; }

        /// <summary>
        /// 目标格式
        /// </summary>
        public string TargetFormat { get; private set; }

        /// <summary>
        /// 转码后的文件路径
        /// </summary>
        public string? TranscodingUrl { get; private set; }

        /// <summary>
        /// 转码状态
        /// </summary>
        public TranscodeStatus TranscodeStatus { get; private set; }

        /// <summary>
        /// 任务消息日志
        /// </summary>
        public string? LogMessage { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; private set; }

        #endregion

        public static TranscodeFile Create(string title, string sourceUrl, string targetFormat)
        {
            var transcodeFile = new TranscodeFile()
            {
                Id = IdGenerateHelper.Instance.GenerateId(),
                Title = title,
                SourceUrl = sourceUrl,
                TargetFormat = targetFormat,
                TranscodeStatus = TranscodeStatus.Ready,
                CreateTime = DateTime.Now,
            };
            transcodeFile.AddDomainEvent(new TranscodeFileCreatedDomainEvent(transcodeFile));
            return transcodeFile;
        }

        public void Start()
        {
            TranscodeStatus = TranscodeStatus.Started;
            AddDomainEvent(new TranscodeFileStartDomainEvent(this));
        }

        public void Complete()
        {
            TranscodeStatus = TranscodeStatus.Completed;
            AddDomainEvent(new TranscodeFileCompleteDomainEvent(this));
        }

        public void Fail(string msg)
        {
            TranscodeStatus = TranscodeStatus.Failed;
            LogMessage = msg;
            AddDomainEvent(new TranscodeFileFailDomainEvent(this));
        }

        public TranscodeFile ChangeFileMeta(long fileSize, string fileSHA256Hash)
        {
            this.FileSizeBytes = fileSize;
            FileSHA256Hash = fileSHA256Hash;
            return this;
        }
    }
}