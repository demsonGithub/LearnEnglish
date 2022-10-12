namespace Demkin.FileOperation.Domain.Interfaces
{
    public interface IStorageFile : IDenpendencyScope
    {
        /// <summary>
        /// 文件的存储类型
        /// </summary>
        StorageType StorageType { get; }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Uri> SaveFileAsync(string cacheKey, string key, Stream content, CancellationToken cancellationToken = default);

        /// <summary>
        /// 上传文件的进度
        /// </summary>
        int GetCompletedPercent();
    }
}