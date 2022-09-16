using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Demkin.Infrastructure.Core
{
    public class EFContext : DbContext, IUnitOfWork
    {
        public EFContext(DbContextOptions options) : base(options)
        {
        }

        #region UnitOfWork

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            Console.WriteLine("执行了SaveChangesAsync");

            return true;
        }

        #endregion UnitOfWork
    }
}