using Demkin.Transcoding.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Transcoding.Infrastructure.EntityConfigurations
{
    internal class TranscodeFileEntityTypeConfiguration : IEntityTypeConfiguration<TranscodeFile>
    {
        public void Configure(EntityTypeBuilder<TranscodeFile> builder)
        {
            builder.ToTable("TranscodeFile");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(e => e.Title).IsUnicode().HasMaxLength(1024);
            builder.Property(e => e.FileSHA256Hash).IsUnicode(false).HasMaxLength(200);

            // 组成符合索引，提高查询效率
            builder.HasIndex(e => new
            {
                e.FileSizeBytes,
                e.FileSHA256Hash
            });
        }
    }
}