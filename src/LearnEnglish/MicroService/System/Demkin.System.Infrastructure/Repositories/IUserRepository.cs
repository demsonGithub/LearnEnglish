using Demkin.Infrastructure.Core;
using Demkin.System.Domain.Entities;

namespace Demkin.System.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository<User, long>
    {
    }
}