using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Core.Api.TenantSwitch.Models.Entities.Resources;

namespace Core.Api.TenantSwitch.DbContexts.Configs
{

    internal class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {

        public void Configure(EntityTypeBuilder<Resource> builder)
        {

            builder.ToTable("Resource");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID");

            builder.Property(x => x.FirstName).HasColumnName("FirstName").IsRequired(true);

            builder.Property(x => x.LastName).HasColumnName("LastName").IsRequired(true);

            builder.Property(x => x.IsDeleted).HasColumnName("IsDeleted").HasColumnType("bit").HasDefaultValue(false);

        }

    }

}