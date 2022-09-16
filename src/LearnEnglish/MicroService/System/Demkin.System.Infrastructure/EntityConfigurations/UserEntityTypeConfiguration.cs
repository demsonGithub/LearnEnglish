using Demkin.System.Domain.Entities;
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
                a.WithOwner();
                a.Property(p => p.Province).HasMaxLength(20);
                a.Property(p => p.City).HasMaxLength(20);
                a.Property(p => p.Area).HasMaxLength(20);
                a.Property(p => p.Street).HasMaxLength(50);
            });
        }
    }
}