using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImperiumLogistics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedRoleColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Company");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
