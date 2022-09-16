using Demkin.Domain.Abstraction;
using System.Linq.Expressions;

namespace Demkin.Infrastructure.Core
{
    public interface IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<bool> RemoveAsync(Entity entity);
    }

    public interface IRepository<TEntity, TKey> : IRepository<TEntity> where TEntity : Entity<TKey>, IAggregateRoot
    {
        Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
    }
}