using Microsoft.EntityFrameworkCore.Migrations;

namespace DepartmentAutomation.Infrastructure.Migrations
{
    public partial class SetStatusToDiscipline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "dbo",
                table: "EducationalPrograms");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "dbo",
                table: "Disciplines",
                type: "int",
                nullable: false,
                defaultValue: 4);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "dbo",
                table: "Disciplines");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "dbo",
                table: "EducationalPrograms",
                type: "int",
                nullable: false,
                defaultValue: 4);
        }
    }
}
