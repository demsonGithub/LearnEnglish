using System.Linq.Expressions;

namespace Demkin.System.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User, long>, IAutofacRegister
    {
        Task<User> GetAsync(Expression<Func<User, bool>> expression);

        Task<IEnumerable<UserRoleRelation>> GetRolesAsync(long userId);
    }
}