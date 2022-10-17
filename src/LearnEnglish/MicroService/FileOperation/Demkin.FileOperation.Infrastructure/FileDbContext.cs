using Microsoft.EntityFrameworkCore;

namespace Demkin.FileOperation.Infrastructure
{
    public class FileDbContext : MyDbContext
    {
        public DbSet<UploadFileInfo> UploadFileInfos { get; set; }

        public FileDbContext(DbContextOptions<FileDbContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UploadFileInfoEntityTypeConfiguration());
        }
    }
}