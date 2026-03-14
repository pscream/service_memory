using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Core.Api.TenantSwitch.Models.Entities.Tenants;

namespace Core.Api.TenantSwitch.DbContexts.Configs
{

    internal class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {

        public void Configure(EntityTypeBuilder<Tenant> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID");

            builder.Property(x => x.Key).HasColumnName("Key");

            builder.Property(x => x.Name).HasColumnName("Name");

            builder.Property(x => x.ConnectionString).HasColumnName("ConnectionString");

            builder.Property(x => x.IsDeleted).HasColumnName("IsDeleted");            

        }

    }

}