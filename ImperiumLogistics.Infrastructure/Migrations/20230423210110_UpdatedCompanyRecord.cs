using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImperiumLogistics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCompanyRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Company",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Company",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Company",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Company",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
