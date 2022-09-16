using Demkin.Infrastructure.Core;
using Demkin.System.Domain.Entities;
using Demkin.System.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Demkin.System.Infrastructure
{
    public class SystemDbContext : EFContext
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}