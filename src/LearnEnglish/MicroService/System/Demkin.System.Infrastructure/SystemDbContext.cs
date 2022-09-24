using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Demkin.System.Infrastructure
{
    public class SystemDbContext : MyDbContext
    {
        private readonly IConfiguration _configuration;

        public SystemDbContext(IMediator mediator, IConfiguration configuration) : base(mediator)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetSection("DbConnection:MasterDb").Value;

            optionsBuilder.UseSqlServer(connectionString, x =>
            {
                x.CommandTimeout(20);
            });
            optionsBuilder.LogTo(new Action<string>(q =>
            {
                if (q.Contains("Executed DbCommand"))
                {
                    Debug.WriteLine(q);
                }
            }), LogLevel.Information);
        }

        public DbSet<User> Users { get; set; } = default!;

        public DbSet<Role> Roles { get; set; } = default!;

        public DbSet<UserRoleRelation> UserRoleRelations { get; set; } = default!;

        public DbSet<Module> Modules { get; set; } = default!;

        public DbSet<RoleModuleRelation> RoleModuleRelations { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguraion());
            modelBuilder.ApplyConfiguration(new UserRoleRelationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ModuleEntityTypeConfiguraion());
            modelBuilder.ApplyConfiguration(new RoleModuleRelationEntityTypeConfiguraion());
        }
    }
}