using Microsoft.EntityFrameworkCore;

namespace Demkin.FileOperation.Infrastructure
{
    public class FileDbContext : MyDbContext
    {
        private readonly IMediator _mediator;

        public DbSet<UploadFileInfo> UploadFileInfos { get; set; }

        public FileDbContext(DbContextOptions options, IMediator mediator) : base(options, mediator)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UploadFileInfoEntityTypeConfiguration());
        }
    }
}