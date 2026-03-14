using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Core.Api.TenantSwitch.Models.Entities.Resources;

namespace Core.Api.TenantSwitch.DbContexts.Configs
{

    internal class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {

        public void Configure(EntityTypeBuilder<Resource> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID");

            builder.Property(x => x.FirstName).HasColumnName("FirstName").IsRequired(true);

            builder.Property(x => x.LastName).HasColumnName("LastName").IsRequired(true);

            builder.Property(x => x.IsDeleted).HasColumnName("IsDeleted").HasColumnType("bit").HasDefaultValue(false);

            builder.HasData(
                    new Resource
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000001"),
                        FirstName = "John",
                        LastName = "Doe",
                        IsDeleted = true,
                    },
                    new Resource
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000002"),
                        FirstName = "Peter",
                        LastName = "First",
                        IsDeleted = true,
                    });

        }

    }

}