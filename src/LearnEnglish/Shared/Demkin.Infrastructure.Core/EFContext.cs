using Demkin.Domain.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Demkin.Infrastructure.Core
{
    public class EFContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public EFContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        #region UnitOfWork

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await SaveChangesAsync(cancellationToken);

            if (result == 0) return false;

            var domainEntities = this.ChangeTracker.Entries<Entity>().Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents).ToList();

            domainEntities.ToList().ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var item in domainEvents)
            {
                await _mediator.Publish(item);
            }

            return true;
        }

        #endregion UnitOfWork
    }
}