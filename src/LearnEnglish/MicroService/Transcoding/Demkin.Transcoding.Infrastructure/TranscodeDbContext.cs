using Demkin.Infrastructure.Core;
using Demkin.Transcoding.Domain;
using Demkin.Transcoding.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Demkin.Transcoding.Infrastructure
{
    public class TranscodeDbContext : MyDbContext
    {
        public TranscodeDbContext(DbContextOptions<TranscodeDbContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        public DbSet<TranscodeFile> TranscodeFile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TranscodeFileEntityTypeConfiguration());
        }
    }
}