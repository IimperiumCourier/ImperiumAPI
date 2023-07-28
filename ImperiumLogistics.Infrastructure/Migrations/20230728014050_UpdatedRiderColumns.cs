using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImperiumLogistics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRiderColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BikeRegistrationNumber",
                table: "Rider",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FrequentLocation",
                table: "Rider",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LicenseNumber",
                table: "Rider",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BikeRegistrationNumber",
                table: "Rider");

            migrationBuilder.DropColumn(
                name: "FrequentLocation",
                table: "Rider");

            migrationBuilder.DropColumn(
                name: "LicenseNumber",
                table: "Rider");
        }
    }
}
