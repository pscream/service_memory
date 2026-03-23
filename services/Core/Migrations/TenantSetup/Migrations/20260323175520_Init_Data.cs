using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TenantSetup.Migrations
{
    /// <inheritdoc />
    public partial class Init_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketStatus",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Key = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Resource_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    AssignedToID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ticket_Resource_AssignedToID",
                        column: x => x.AssignedToID,
                        principalTable: "Resource",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Ticket_TicketStatus_StatusID",
                        column: x => x.StatusID,
                        principalTable: "TicketStatus",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Ticket_User_CreatedByID",
                        column: x => x.CreatedByID,
                        principalTable: "User",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Ticket_User_UpdatedByID",
                        column: x => x.UpdatedByID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "ID", "FirstName", "LastName", "UserID" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Peter", "Black", null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Robert", "Johnson", null },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Emily", "Davis", null },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Christopher", "Miller", null },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "David", "Moore", null }
                });

            migrationBuilder.InsertData(
                table: "TicketStatus",
                columns: new[] { "ID", "Key", "Name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), 100, "New" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), 200, "InProgress" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), 300, "Completed" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "FirstName", "LastName", "Password", "Username" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "John", "Doe", "", "john.doe@example.com" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Alice", "Smith", "", "alice.smith@example.com" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Sarah", "Williams", "", "sarah.williams@example.com" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Michael", "Brown", "", "michael.brown@example.com" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Jessica", "Wilson", "", "jessica.wilson@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "ID", "FirstName", "LastName", "UserID" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "John", "Doe", new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Alice", "Smith", new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Sarah", "Williams", new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Michael", "Brown", new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Jessica", "Wilson", new Guid("00000000-0000-0000-0000-000000000005") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resource_UserID",
                table: "Resource",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_AssignedToID",
                table: "Ticket",
                column: "AssignedToID");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CreatedByID",
                table: "Ticket",
                column: "CreatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_StatusID",
                table: "Ticket",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UpdatedByID",
                table: "Ticket",
                column: "UpdatedByID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "TicketStatus");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
