using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Listen.Infrastructure.Repositories
{
    public class EpisodeRepository : Repository<Episode, long>, IEpisodeRepository
    {
        public EpisodeRepository(ListenDbContext dbContext) : base(dbContext)
        {
        }
    }
}