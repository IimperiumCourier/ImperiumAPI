using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImperiumLogistics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRefreshTokenColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Company",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Company",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Company");
        }
    }
}
