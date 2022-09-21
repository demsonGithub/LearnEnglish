using Microsoft.EntityFrameworkCore;

namespace Demkin.Listen.Infrastructure
{
    public class ListenDbContext : EFContext
    {
        public ListenDbContext(DbContextOptions options, IMediator mediator) : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}