using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Demkin.FileOperation.Infrastructure
{
    public class FileDbContext : MyDbContext
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public DbSet<UploadFileInfo> UploadFileInfos { get; set; }

        public FileDbContext(IMediator mediator, IConfiguration configuration) : base(mediator)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetSection("DbConnection:MasterDb").Value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UploadFileInfoEntityTypeConfiguration());
        }
    }
}