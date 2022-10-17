namespace Demkin.Listen.WebApi.Admin.Application.Models
{
    public enum TranscodeStatus
    {
        /// <summary>
        /// 任务创建完成
        /// </summary>
        Ready = 0,

        /// <summary>
        /// 开始处理转码
        /// </summary>
        Started = 1,

        /// <summary>
        /// 任务成功完成
        /// </summary>
        Completed = 2,

        /// <summary>
        /// 失败
        /// </summary>
        Failed = 3
    }
}