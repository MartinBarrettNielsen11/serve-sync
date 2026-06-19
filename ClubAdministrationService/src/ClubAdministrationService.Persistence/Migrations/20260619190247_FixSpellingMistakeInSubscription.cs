using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubAdministrationService.Persistence.Migrations
{
    /// <inheritdoc />
#pragma warning disable MA0048
    public partial class FixSpellingMistakeInSubscription : Migration
#pragma warning restore MA0048
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClubsIds",
                table: "Subscriptions");

            migrationBuilder.AddColumn<string>(
                name: "ClubIds",
                table: "Subscriptions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClubIds",
                table: "Subscriptions");

            migrationBuilder.AddColumn<string>(
                name: "ClubsIds",
                table: "Subscriptions",
                type: "text",
                nullable: true);
        }
    }
}
