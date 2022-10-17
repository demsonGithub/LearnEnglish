using Demkin.Listen.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace Demkin.Listen.Infrastructure
{
    public class ListenDbContext : MyDbContext
    {
        private readonly string _connectionStrings;

        public ListenDbContext(DbContextOptions<ListenDbContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        public ListenDbContext(IMediator mediator, string connectionStrings) : base(mediator)
        {
            _connectionStrings = connectionStrings;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Episode> Audios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(_connectionStrings))
            {
                optionsBuilder.UseSqlServer(_connectionStrings);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EpisodeEntityTypeConfiguration());
        }
    }
}