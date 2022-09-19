using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demkin.System.Infrastructure.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.UserName).HasMaxLength(20);
            builder.Property(x => x.Password).HasMaxLength(30);

            builder.OwnsOne(o => o.Address, a =>
            {
                a.Property(p => p.Province).IsRequired(true).HasMaxLength(20);
                a.Property(p => p.City).IsRequired(false).HasMaxLength(20);
                a.Property(p => p.Area).IsRequired(false).HasMaxLength(20);
                a.Property(p => p.Street).IsRequired(false).HasMaxLength(50);
            });
        }
    }
}