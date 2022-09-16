using Demkin.Infrastructure.Core;
using System.Linq.Expressions;

namespace Demkin.System.Infrastructure.Repositories
{
    public class UserRepository : Repository<User, long, SystemDbContext>, IUserRepository
    {
        private readonly SystemDbContext _dbContext;

        public UserRepository(SystemDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            return await Task.Run(() =>
            {
                var result = _dbContext.Users.FirstOrDefault(expression);
                return result;
            });
        }
    }
}