using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Core.Api.TenantSwitch.Models.Entities.Tickets;

namespace Core.Api.TenantSwitch.DbContexts.Configs.Tickets
{

    internal class TicketStatusConfiguration : IEntityTypeConfiguration<TicketStatus>
    {

        public void Configure(EntityTypeBuilder<TicketStatus> builder)
        {
            builder.ToTable("TicketStatus");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID");

            builder.Property(x => x.Name).HasColumnName("Name")
                    .IsRequired(true);

            builder.Property(x => x.Key).HasColumnName("Key")
                    .IsRequired(true);

            builder.Property(x => x.IsDeleted).HasColumnName("IsDeleted")
                    .HasDefaultValue(false);
        }

    }

}