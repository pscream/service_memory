using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Core.Migrations.TenantSetup.Entities;

namespace Core.Migrations.TenantSetup.DbContexts.Configs
{

    internal class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {

        public void Configure(EntityTypeBuilder<Resource> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID");

            builder.Property(x => x.FirstName).HasColumnName("FirstName")
                    .HasColumnType("nvarchar(100)")
                    .IsRequired(true);

            builder.Property(x => x.LastName).HasColumnName("LastName")
                    .HasColumnType("nvarchar(100)")
                    .IsRequired(true);

            builder.Property(x => x.UserId).HasColumnName("UserID")
                    .IsRequired(false);
            builder.HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.IsDeleted).HasColumnName("IsDeleted")
                    .HasColumnType("bit").HasDefaultValue(false);

            builder.HasData(
                    new Resource
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000001"),
                        FirstName = "John",
                        LastName = "Doe",
                        UserId = new Guid("00000000-0000-0000-0000-000000000001"),
                    },
                    new Resource
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000002"),
                        FirstName = "Peter",
                        LastName = "Black",
                        UserId = null,
                    },
                    new Resource
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000003"),
                        FirstName = "Alice",
                        LastName = "Smith",
                        UserId = new Guid("00000000-0000-0000-0000-000000000002"),
                    },
                    new Resource
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000004"),
                        FirstName = "Robert",
                        LastName = "Johnson",
                        UserId = null,
                    },
                    new Resource
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000005"),
                        FirstName = "Sarah",
                        LastName = "Williams",
                        UserId = new Guid("00000000-0000-0000-0000-000000000003"),
                    },
                    new Resource
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000006"),
                        FirstName = "Michael",
                        LastName = "Brown",
                        UserId = new Guid("00000000-0000-0000-0000-000000000004"),
                    },
                    new Resource
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000007"),
                        FirstName = "Emily",
                        LastName = "Davis",
                        UserId = null,
                    },
                    new Resource
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000008"),
                        FirstName = "Christopher",
                        LastName = "Miller",
                        UserId = null,
                    },
                    new Resource
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000009"),
                        FirstName = "Jessica",
                        LastName = "Wilson",
                        UserId = new Guid("00000000-0000-0000-0000-000000000005"),
                    },
                    new Resource
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000010"),
                        FirstName = "David",
                        LastName = "Moore",
                        UserId = null,
                    });

        }

    }

}