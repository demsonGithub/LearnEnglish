using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demkin.System.Infrastructure.EntityConfigurations
{
    internal class UserRoleRelationEntityTypeConfiguration : IEntityTypeConfiguration<UserRoleRelation>
    {
        public void Configure(EntityTypeBuilder<UserRoleRelation> builder)
        {
            builder.ToTable("UserRoleRelation");
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}