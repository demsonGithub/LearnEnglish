using Demkin.Infrastructure.Core;
using System.Linq.Expressions;

namespace Demkin.System.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository<User, long>
    {
        Task<User> GetAsync(Expression<Func<User, bool>> expression);
    }
}