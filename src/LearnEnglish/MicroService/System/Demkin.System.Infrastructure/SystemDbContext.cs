using Demkin.Infrastructure.Core;

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

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRoleRelation> UserRoleRelations { get; set; }

        public DbSet<Module> Modules { get; set; }

        public DbSet<RoleModuleRelation> RoleModuleRelations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguraion());
            modelBuilder.ApplyConfiguration(new UserRoleRelationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ModuleEntityTypeConfiguraion());
            modelBuilder.ApplyConfiguration(new RoleModuleRelationEntityTypeConfiguraion());

            //base.OnModelCreating(modelBuilder);
        }
    }
}