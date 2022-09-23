using Demkin.Listen.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Listen.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category, long>, ICategoryRepository
    {
        private readonly IDbContextFactory _dbContextFactory;

        public CategoryRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
    }
}