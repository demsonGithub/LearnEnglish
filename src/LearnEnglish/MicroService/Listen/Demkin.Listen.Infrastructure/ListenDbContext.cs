﻿using Demkin.Listen.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace Demkin.Listen.Infrastructure
{
    public class ListenDbContext : MyDbContext
    {
        private readonly string _connectionStrings;

        public ListenDbContext(IMediator mediator, IConfiguration configuration) : base(mediator)
        {
            _connectionStrings = configuration.GetSection("DbConnection:MasterDb").Value;
        }

        public ListenDbContext(IMediator mediator, string connectionStrings) : base(mediator)
        {
            _connectionStrings = connectionStrings;
        }

        private DbSet<Category> Categories { get; set; }
        private DbSet<Album> Albums { get; set; }
        private DbSet<Audio> Audios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionStrings);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AudioEntityTypeConfiguration());
        }
    }
}