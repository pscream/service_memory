using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TenantSetup.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Tickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var assignedToIds = new[]
                        {
                new Guid("00000000-0000-0000-0000-000000000001"),
                new Guid("00000000-0000-0000-0000-000000000002"),
                new Guid("00000000-0000-0000-0000-000000000003"),
                new Guid("00000000-0000-0000-0000-000000000004"),
                new Guid("00000000-0000-0000-0000-000000000005"),
            };

            var createdByIds = new[]
            {
                new Guid("00000000-0000-0000-0000-000000000001"),
                new Guid("00000000-0000-0000-0000-000000000002"),
                new Guid("00000000-0000-0000-0000-000000000003"),
                new Guid("00000000-0000-0000-0000-000000000004"),
                new Guid("00000000-0000-0000-0000-000000000005"),
            };

            var createdDate = new DateTime(2026, 3, 22);

            for (int i = 1; i <= 100; i++)
            {
                var code = i.ToString("D8");
                var isAssigned = i % 5 == 0;
                var assignedToIndex = isAssigned ? (i / 5 - 1) % 5 : -1;
                var assignedToId = isAssigned ? assignedToIds[assignedToIndex] : (Guid?)null;
                var statusId = isAssigned
                    ? new Guid("00000000-0000-0000-0000-000000000002")
                    : new Guid("00000000-0000-0000-0000-000000000001");
                var createdById = createdByIds[(i - 1) % 5];
                var description = $"Ticket '{code}'";

                migrationBuilder.InsertData(
                    table: "Ticket",
                    columns: new[] { "ID", "Code", "Description", "AssignedToID", "StatusID", "CreatedDate", "CreatedByID", "IsDeleted" },
                    values: new object[]
                    {
                        Guid.NewGuid(),
                        code,
                        description,
                        assignedToId,
                        statusId,
                        createdDate,
                        createdById,
                        false
                    }
                );
            }

            // Update descriptions for assigned tickets with user names
            migrationBuilder.Sql(@"
                UPDATE t
                SET t.Description = CONCAT('Ticket ''', t.Code, ''', assigned to ', u.LastName, ', ', u.FirstName)
                FROM Ticket t
                INNER JOIN [User] u ON t.AssignedToID = u.ID
                WHERE t.AssignedToID IS NOT NULL;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
