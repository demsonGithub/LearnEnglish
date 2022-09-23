using Demkin.Listen.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Listen.Infrastructure
{
    public class ListenDbContext2 : MyDbContext
    {
        public ListenDbContext2(DbContextOptions<ListenDbContext2> options, IMediator mediator) : base(options, mediator)
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