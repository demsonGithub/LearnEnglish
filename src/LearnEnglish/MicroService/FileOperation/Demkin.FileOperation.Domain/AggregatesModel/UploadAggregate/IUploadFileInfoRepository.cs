namespace Demkin.FileOperation.Domain.AggregatesModel.UploadAggregate
{
    public interface IUploadFileInfoRepository : IRepository<UploadFileInfo, long>
    {
        Task<UploadFileInfo> FindFileAsync(long fileSize, string hash256);
    }
}