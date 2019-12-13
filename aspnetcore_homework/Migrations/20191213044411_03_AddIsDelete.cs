using Microsoft.EntityFrameworkCore.Migrations;

namespace aspnetcore_homework.Migrations
{
    public partial class _03_AddIsDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Person",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Department",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Course",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Course");
        }
    }
}
