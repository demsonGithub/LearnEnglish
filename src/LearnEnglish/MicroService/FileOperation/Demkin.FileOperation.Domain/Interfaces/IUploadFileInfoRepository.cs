namespace Demkin.FileOperation.Domain.Interfaces
{
    public interface IUploadFileInfoRepository : IRepository<UploadFileInfo, long>
    {
        Task<UploadFileInfo> FindFileAsync(long fileSize, string hash256);
    }
}