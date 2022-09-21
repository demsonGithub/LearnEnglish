using Demkin.Domain.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Demkin.Infrastructure.Core
{
    public abstract class Repository<TEntity, TDbContext> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot where TDbContext : EFContext
    {
        protected virtual TDbContext DbContext { get; private set; }

        protected Repository(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        // EFContext实现了IUnitOfWork
        public IUnitOfWork UnitOfWork => DbContext;

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var result = await DbContext.AddAsync(entity, cancellationToken);
            return result.Entity;
        }

        public virtual async Task<bool> RemoveAsync(Entity entity, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                DbContext.Remove(entity);
                return true;
            });
        }

        public virtual async Task<bool> RemoveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                DbContext.RemoveRange(predicate, cancellationToken);
                return true;
            });
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var result = await DbContext.Set<TEntity>().FindAsync(predicate);

            return result != null;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                var result = DbContext.Update(entity).Entity;
                return result;
            });
        }
    }

    public abstract class Repository<TEntity, TKey, TDbContext> : Repository<TEntity, TDbContext>, IRepository<TEntity, TKey> where TEntity : Entity<TKey>, IAggregateRoot where TDbContext : EFContext
    {
        public Repository(TDbContext dbContext) : base(dbContext)
        {
        }

        public virtual async Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await DbContext.FindAsync<TEntity>(id, cancellationToken);

            return entity;
        }

        public virtual async Task<bool> RemoveAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await DbContext.FindAsync<TEntity>(id, cancellationToken);
            if (entity == null)
            {
                return false;
            }
            DbContext.Remove(entity);
            return true;
        }
    }
}