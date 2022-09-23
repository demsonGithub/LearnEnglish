using Demkin.Domain.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Demkin.System.Infrastructure.Repositories
{
    public class UserRepository : Repository<User, long>, IUserRepository
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

        public async Task<IEnumerable<UserRoleRelation>> GetRolesAsync(long userId)
        {
            var result = await _dbContext.UserRoleRelations.Include(c => c.User).Include(c => c.Role).Where(item => item.UserId == userId).ToListAsync();

            return result;
        }
    }
}