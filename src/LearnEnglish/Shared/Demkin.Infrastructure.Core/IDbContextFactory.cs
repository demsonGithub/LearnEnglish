using Demkin.Domain.Abstraction;

namespace Demkin.Infrastructure.Core
{
    public interface IDbContextFactory : IDenpendencySingleton
    {
        MyDbContext CreateDbContext(ReadAndWrite readAndWrite = ReadAndWrite.Write);
    }

    public enum ReadAndWrite
    {
        Read,
        Write
    }
}