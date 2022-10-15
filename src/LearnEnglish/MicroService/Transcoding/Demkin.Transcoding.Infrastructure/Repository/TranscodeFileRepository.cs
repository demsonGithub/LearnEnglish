using Demkin.Infrastructure.Core;
using Demkin.Transcoding.Domain;
using Demkin.Transcoding.Domain.Interfaces;

namespace Demkin.Transcoding.Infrastructure.Repository
{
    public class TranscodeFileRepository : Repository<TranscodeFile, long>, ITranscodeFileRepository
    {
        public TranscodeFileRepository(TranscodeDbContext dbContext) : base(dbContext)
        {
        }
    }
}