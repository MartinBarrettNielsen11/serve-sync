using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubAdministrationService.Persistence.Migrations
{
    /// <inheritdoc />
#pragma warning disable MA0048
    public partial class AddSubscriptionTable : Migration
#pragma warning restore MA0048
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxCourtsAllowed",
                table: "Subscriptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionType",
                table: "Subscriptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxCourtsAllowed",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionType",
                table: "Subscriptions");
        }
    }
}
