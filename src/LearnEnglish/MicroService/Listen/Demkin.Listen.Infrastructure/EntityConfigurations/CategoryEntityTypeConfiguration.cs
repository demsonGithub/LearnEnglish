using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Listen.Infrastructure.EntityConfigurations
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.OwnsOne(o => o.MultipleLanguageTitle, x =>
            {
                x.Property(y => y.ChineseTitle).HasColumnName("ChineseTitle").IsRequired(true).HasMaxLength(50);
                x.Property(y => y.EnglishTitle).HasColumnName("EnglishTitle").IsRequired(true).HasMaxLength(50);
            });
        }
    }
}