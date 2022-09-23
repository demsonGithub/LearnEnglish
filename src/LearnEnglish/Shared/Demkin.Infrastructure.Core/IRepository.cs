using Demkin.Infrastructure.Core;
using System.Linq.Expressions;

namespace Demkin.Domain.Abstraction
{
    public interface IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        MyDbContext Db { get; }

        Task ChangeDb(ReadAndWrite readAndWrite);

        IUnitOfWork UnitOfWork { get; }

        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<bool> RemoveAsync(Entity entity, CancellationToken cancellationToken = default);

        Task<bool> RemoveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }

    public interface IRepository<TEntity, TKey> : IRepository<TEntity> where TEntity : Entity<TKey>, IAggregateRoot
    {
        Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken = default);

        Task<bool> RemoveAsync(TKey id, CancellationToken cancellationToken = default);
    }
}