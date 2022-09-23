using Demkin.Domain.Abstraction;

namespace Demkin.Infrastructure.Core
{
    public interface IDbContextFactory : IDenpendencySingleton
    {
        MyDbContext CreateMasterDbContext();

        MyDbContext CreateSlaveDbContext();
    }
}