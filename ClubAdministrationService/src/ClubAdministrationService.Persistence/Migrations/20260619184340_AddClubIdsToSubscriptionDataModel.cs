using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubAdministrationService.Persistence.Migrations
{
    /// <inheritdoc />
#pragma warning disable MA0048
    public partial class AddClubIdsToSubscriptionDataModel : Migration
#pragma warning restore MA0048
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClubsIds",
                table: "Subscriptions",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClubsIds",
                table: "Subscriptions");
        }
    }
}
