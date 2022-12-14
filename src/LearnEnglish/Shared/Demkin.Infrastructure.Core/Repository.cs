using Demkin.Domain.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Demkin.Infrastructure.Core
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        private MyDbContext _db;
        public MyDbContext Db => _db;

        protected Repository(MyDbContext dbContext)
        {
            _db = dbContext;
        }

        // EFContext实现了IUnitOfWork
        public IUnitOfWork UnitOfWork => _db;

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var result = await _db.AddAsync(entity, cancellationToken);
            return result.Entity;
        }

        public virtual async Task<bool> RemoveAsync(Entity entity, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                _db.Remove(entity);
                return true;
            });
        }

        public virtual async Task<bool> RemoveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                _db.RemoveRange(predicate, cancellationToken);
                return true;
            });
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _db.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _db.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var result = await _db.Set<TEntity>().AnyAsync(predicate, cancellationToken);

            return result;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                var result = _db.Update(entity).Entity;
                return result;
            });
        }
    }

    public abstract class Repository<TEntity, TKey> : Repository<TEntity>, IRepository<TEntity, TKey> where TEntity : Entity<TKey>, IAggregateRoot
    {
        protected Repository(MyDbContext dbContext) : base(dbContext)
        {
        }

        public virtual async Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await Db.FindAsync<TEntity>(new object[] { id }, cancellationToken);

            return entity;
        }

        public virtual async Task<bool> RemoveAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await Db.FindAsync<TEntity>(new object[] { id }, cancellationToken);
            if (entity == null)
            {
                return false;
            }
            Db.Remove(entity);
            return true;
        }
    }
}