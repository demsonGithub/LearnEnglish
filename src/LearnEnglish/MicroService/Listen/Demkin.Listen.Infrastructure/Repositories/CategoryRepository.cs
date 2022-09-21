using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Listen.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category, long, ListenDbContext>, ICategoryRepository
    {
        private readonly ListenDbContext _dbContext;

        public CategoryRepository(ListenDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}