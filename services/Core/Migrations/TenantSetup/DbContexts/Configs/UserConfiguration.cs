using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Core.Migrations.TenantSetup.Entities;

namespace Core.Migrations.TenantSetup.DbContexts.Configs
{

    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID");

            builder.Property(x => x.Username).HasColumnName("Username")
                    .HasColumnType("nvarchar(100)")
                    .IsRequired(true);

            builder.Property(x => x.Password).HasColumnName("Password")
                    .HasColumnType("nvarchar(300)")
                    .IsRequired(true);

            builder.Property(x => x.FirstName).HasColumnName("FirstName")
                    .HasColumnType("nvarchar(100)")
                    .IsRequired(true);

            builder.Property(x => x.LastName).HasColumnName("LastName")
                    .HasColumnType("nvarchar(100)")
                    .IsRequired(true);

            builder.Property(x => x.IsDeleted).HasColumnName("IsDeleted")
                    .HasColumnType("bit").HasDefaultValue(false);

            builder.HasData(
                    new User
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000001"),
                        Username = "john.doe@example.com",
                        Password = string.Empty,
                        FirstName = "John",
                        LastName = "Doe",
                    },
                    new User
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000002"),
                        Username = "alice.smith@example.com",
                        Password = string.Empty,
                        FirstName = "Alice",
                        LastName = "Smith",
                    },
                    new User
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000003"),
                        Username = "sarah.williams@example.com",
                        Password = string.Empty,
                        FirstName = "Sarah",
                        LastName = "Williams",
                    },
                    new User
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000004"),
                        Username = "michael.brown@example.com",
                        Password = string.Empty,
                        FirstName = "Michael",
                        LastName = "Brown",
                    },
                    new User
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000005"),
                        Username = "jessica.wilson@example.com",
                        Password = string.Empty,
                        FirstName = "Jessica",
                        LastName = "Wilson",
                    });

        }

    }

}
