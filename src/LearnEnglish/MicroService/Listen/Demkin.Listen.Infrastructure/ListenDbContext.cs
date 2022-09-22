using Demkin.Listen.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Demkin.Listen.Infrastructure
{
    public class ListenDbContext : EFContext
    {
        public ListenDbContext(DbContextOptions options, IMediator mediator) : base(options, mediator)
        {
        }

        private DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
        }
    }
}