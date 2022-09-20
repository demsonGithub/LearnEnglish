using Demkin.Infrastructure.Core;

namespace Demkin.FileOperation.Infrastructure.Repositories
{
    public class UploadFileInfoRepository : Repository<UploadFileInfo, long, FileDbContext>, IUploadFileInfoRepository
    {
        private readonly FileDbContext _dbContext;

        public UploadFileInfoRepository(FileDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}