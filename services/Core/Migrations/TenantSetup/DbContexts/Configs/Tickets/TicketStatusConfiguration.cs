using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Core.Global.Enums;
using Core.Migrations.TenantSetup.Entities.Tickets;

namespace Core.Migrations.TenantSetup.DbContexts.Configs.Tickets
{

    internal class TicketStatusConfiguration : IEntityTypeConfiguration<TicketStatus>
    {

        public void Configure(EntityTypeBuilder<TicketStatus> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID");

            builder.Property(x => x.Name).HasColumnName("Name")
                    .HasColumnType("nvarchar(50)")
                    .IsRequired(true);

            builder.Property(x => x.Key).HasColumnName("Key")
                    .HasColumnType("int")
                    .IsRequired(true);

            builder.Property(x => x.IsDeleted).HasColumnName("IsDeleted")
                    .HasColumnType("bit")
                    .HasDefaultValue(false);

            builder.HasData(
                    new TicketStatus
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000001"),
                        Name = "New",
                        Key = TicketStatusKeys.New,
                    },
                    new TicketStatus
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000002"),
                        Name = "InProgress",
                        Key = TicketStatusKeys.InProgress,
                    },
                    new TicketStatus
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000003"),
                        Name = "Completed",
                        Key = TicketStatusKeys.Completed,
                    });

        }

    }

}