namespace Demkin.Listen.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category, long>, ICategoryRepository
    {
        private readonly ListenDbContext _dbContext;

        public CategoryRepository(ListenDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}