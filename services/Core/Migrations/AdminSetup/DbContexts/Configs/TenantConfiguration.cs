using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Core.Migrations.AdminSetup.Entities;

namespace Core.Migrations.AdminSetup.DbContexts.Configs
{

    internal class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {

        public void Configure(EntityTypeBuilder<Tenant> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID");

            builder.Property(x => x.Key).HasColumnName("Key").IsRequired(true);

            builder.Property(x => x.Name).HasColumnName("Name").IsRequired(true);

            builder.Property(x => x.ConnectionString).HasColumnName("ConnectionString").IsRequired(true);

            builder.Property(x => x.IsDeleted).HasColumnName("IsDeleted").HasColumnType("bit").HasDefaultValue(false);

            builder.HasData(
                    new Tenant
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000001"),
                        Name = "Deep Blue Tenant",
                        Key = "deep-blue-tenant",
                        ConnectionString = Global.Constant.Tenant.DeepBlueDatabaseTag,
                        IsDeleted = true,
                    },
                    new Tenant
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000002"),
                        Name = "Storm Shark Tenant",
                        Key = "storm-shark-tenant",
                        ConnectionString = Global.Constant.Tenant.StormSharkDatabaseTag,
                        IsDeleted = true,
                    });

        }

    }

}