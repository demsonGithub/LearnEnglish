using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Listen.Infrastructure.Repositories
{
    public class AlbumRepository : Repository<Album, long>, IAlbumRepository
    {
        public AlbumRepository(ListenDbContext dbContext) : base(dbContext)
        {
        }
    }
}