using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Core.Migrations.TenantSetup.Entities.Tickets;

namespace Core.Migrations.TenantSetup.DbContexts.Configs.Tickets
{

        internal class TicketConfiguration : IEntityTypeConfiguration<Ticket>
        {

                public void Configure(EntityTypeBuilder<Ticket> builder)
                {

                        builder.ToTable("Ticket");

                        builder.HasKey(x => x.Id);

                        builder.Property(x => x.Id).HasColumnName("ID");

                        builder.Property(x => x.Code).HasColumnName("Code")
                                .HasColumnType("nvarchar(100)")
                                .IsRequired(true);

                        builder.Property(x => x.Description).HasColumnName("Description")
                                .HasColumnType("nvarchar(1000)")
                                .IsRequired(true);

                        builder.Property(x => x.AssignedToId).HasColumnName("AssignedToID");
                        builder.HasOne(x => x.AssignedTo)
                                .WithMany()
                                .HasForeignKey(x => x.AssignedToId)
                                .OnDelete(DeleteBehavior.NoAction);

                        builder.Property(x => x.StatusId).HasColumnName("StatusID")
                                .IsRequired(true);
                        builder.HasOne(x => x.Status)
                                .WithMany()
                                .HasForeignKey(x => x.StatusId)
                                .OnDelete(DeleteBehavior.NoAction);

                        builder.Property(x => x.CreatedDate).HasColumnName("CreatedDate")
                                .HasColumnType("datetime2")
                                .IsRequired(true);
                        builder.Property(x => x.CreatedById).HasColumnName("CreatedByID")
                                .IsRequired(true);
                        builder.HasOne(x => x.CreatedBy)
                                .WithMany()
                                .HasForeignKey(x => x.CreatedById)
                                .OnDelete(DeleteBehavior.NoAction);

                        builder.Property(x => x.UpdatedDate).HasColumnName("UpdatedDate")
                                .HasColumnType("datetime2")
                                .IsRequired(false);
                        builder.Property(x => x.UpdatedById).HasColumnName("UpdatedByID");
                        builder.HasOne(x => x.UpdatedBy)
                                .WithMany()
                                .HasForeignKey(x => x.UpdatedById)
                                .OnDelete(DeleteBehavior.NoAction);

                        builder.Property(x => x.IsDeleted).HasColumnName("IsDeleted").HasColumnType("bit").HasDefaultValue(false);

                }

        }

}
