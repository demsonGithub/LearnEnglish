using Demkin.FileOperation.Infrastructure.EntityConfigurations;
using Demkin.Infrastructure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Demkin.FileOperation.Infrastructure
{
    public class FileDbContext : EFContext
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