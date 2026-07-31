using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserAdministrationService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameOutboxIntegrationEventTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OutboxIntegrationEvent",
                table: "OutboxIntegrationEvent");

            migrationBuilder.RenameTable(
                name: "OutboxIntegrationEvent",
                newName: "OutboxIntegrationEvents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OutboxIntegrationEvents",
                table: "OutboxIntegrationEvents",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OutboxIntegrationEvents",
                table: "OutboxIntegrationEvents");

            migrationBuilder.RenameTable(
                name: "OutboxIntegrationEvents",
                newName: "OutboxIntegrationEvent");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OutboxIntegrationEvent",
                table: "OutboxIntegrationEvent",
                column: "Id");
        }
    }
}
