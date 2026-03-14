using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Core.Migrations.TenantSetup.Entities;

namespace Core.Migrations.TenantSetup.DbContexts.Configs
{

    internal class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {

        public void Configure(EntityTypeBuilder<Tenant> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID");

            builder.Property(x => x.ConnectionString).HasColumnName("ConnectionString").IsRequired(true);

            builder.Property(x => x.IsDeleted).HasColumnName("IsDeleted").HasColumnType("bit");

        }

    }

}