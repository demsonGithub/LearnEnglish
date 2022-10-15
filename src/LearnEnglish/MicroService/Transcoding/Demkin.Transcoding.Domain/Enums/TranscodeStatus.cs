namespace Demkin.Transcoding.Domain
{
    public enum TranscodeStatus
    {
        /// <summary>
        /// 任务创建完成
        /// </summary>
        Ready,

        /// <summary>
        /// 开始处理转码
        /// </summary>
        Started,

        /// <summary>
        /// 任务成功完成
        /// </summary>
        Completed,

        /// <summary>
        /// 失败
        /// </summary>
        Failed
    }
}