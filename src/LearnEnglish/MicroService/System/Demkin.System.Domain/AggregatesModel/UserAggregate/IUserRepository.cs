using System.Linq.Expressions;

namespace Demkin.System.Domain.AggregatesModel.UserAggregate
{
    public interface IUserRepository : IRepository<User, long>
    {
        Task<User> GetAsync(Expression<Func<User, bool>> expression);

        Task<IEnumerable<UserRoleRelation>> GetRolesAsync(long userId);
    }
}