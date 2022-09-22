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
        private DbSet<Album> Albums { get; set; }
        private DbSet<Audio> Audios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AudioEntityTypeConfiguration());
        }
    }
}