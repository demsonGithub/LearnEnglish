using Demkin.Infrastructure.Core;
using Demkin.System.Domain.Entities;

namespace Demkin.System.Infrastructure.Repositories
{
    public class UserRepository : Repository<User, long, SystemDbContext>, IUserRepository
    {
        public UserRepository(SystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}