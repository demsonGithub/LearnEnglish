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
        /// <param name="fileName">文件名，带后缀</param>
        /// <param name="content">文件流</param>
        /// <param name="cacheKey">缓存的Key</param>
        /// <param name="cancellationToken"></param>
        /// <returns>相对路径</returns>
        Task<string> SaveFileAsync(string fileName, Stream content, string cacheKey, CancellationToken cancellationToken = default);

        /// <summary>
        /// 上传文件的进度
        /// </summary>
        int GetCompletedPercent();
    }
}