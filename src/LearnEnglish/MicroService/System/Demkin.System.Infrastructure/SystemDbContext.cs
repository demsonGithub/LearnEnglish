using Demkin.Infrastructure.Core;

using Demkin.System.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Demkin.System.Infrastructure
{
    public class SystemDbContext : EFContext
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        public DbSet<User> Users { get; set; } = default!;

        public DbSet<Role> Roles { get; set; } = default!;

        public DbSet<UserRoleRelation> UserRoleRelations { get; set; } = default!;

        public DbSet<Module> Modules { get; set; } = default!;

        public DbSet<RoleModuleRelation> RoleModuleRelations { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguraion());
            modelBuilder.ApplyConfiguration(new UserRoleRelationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ModuleEntityTypeConfiguraion());
            modelBuilder.ApplyConfiguration(new RoleModuleRelationEntityTypeConfiguraion());

            base.OnModelCreating(modelBuilder);
        }
    }
}