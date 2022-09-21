namespace Demkin.FileOperation.Domain.Interfaces
{
    public interface IStorageFile
    {
        /// <summary>
        /// 文件的存储类型
        /// </summary>
        StorageType StorageType { get; }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Uri> SaveFileAsync(string key, Stream content, CancellationToken cancellationToken = default);
    }
}