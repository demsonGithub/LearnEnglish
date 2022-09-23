using Microsoft.EntityFrameworkCore;

namespace Demkin.FileOperation.Infrastructure.Repositories
{
    public class UploadFileInfoRepository : Repository<UploadFileInfo, long>, IUploadFileInfoRepository
    {
        private readonly FileDbContext _dbContext;

        public UploadFileInfoRepository(FileDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UploadFileInfo> FindFileAsync(long fileSize, string hash256)
        {
            var result = await _dbContext.UploadFileInfos.FirstOrDefaultAsync(item => item.FileSizeBytes == fileSize && item.FileSHA256Hash == hash256);

            return result;
        }
    }
}