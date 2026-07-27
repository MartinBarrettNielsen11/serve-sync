using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubAdministrationService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddListOfIdsToClubEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InstructorIds",
                table: "Clubs",
                type: "text",
                nullable: false,
                oldClrType: typeof(List<Guid>),
                oldType: "uuid[]");

            migrationBuilder.AlterColumn<string>(
                name: "CourtIds",
                table: "Clubs",
                type: "text",
                nullable: false,
                oldClrType: typeof(List<Guid>),
                oldType: "uuid[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<Guid>>(
                name: "InstructorIds",
                table: "Clubs",
                type: "uuid[]",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<List<Guid>>(
                name: "CourtIds",
                table: "Clubs",
                type: "uuid[]",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
