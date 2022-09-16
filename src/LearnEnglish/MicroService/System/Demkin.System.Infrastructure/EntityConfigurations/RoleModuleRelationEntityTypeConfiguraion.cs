using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demkin.System.Infrastructure.EntityConfigurations
{
    internal class RoleModuleRelationEntityTypeConfiguraion : IEntityTypeConfiguration<RoleModuleRelation>
    {
        public void Configure(EntityTypeBuilder<RoleModuleRelation> builder)
        {
            builder.ToTable("RoleModuleRelation");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}