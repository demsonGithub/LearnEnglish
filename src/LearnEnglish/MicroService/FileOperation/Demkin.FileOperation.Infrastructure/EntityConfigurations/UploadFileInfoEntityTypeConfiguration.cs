using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demkin.FileOperation.Infrastructure.EntityConfigurations
{
    public class UploadFileInfoEntityTypeConfiguration : IEntityTypeConfiguration<UploadFileInfo>
    {
        public void Configure(EntityTypeBuilder<UploadFileInfo> builder)
        {
            builder.ToTable("UploadFileInfo");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.FileName).IsUnicode().HasMaxLength(1024);
            builder.Property(e => e.FileSHA256Hash).IsUnicode(false).HasMaxLength(60);

            // 组成符合索引，提高查询效率
            builder.HasIndex(e => new
            {
                e.FileSizeBytes,
                e.FileSHA256Hash
            });
        }
    }
}