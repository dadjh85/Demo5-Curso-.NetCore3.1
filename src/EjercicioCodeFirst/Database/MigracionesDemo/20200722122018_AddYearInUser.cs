using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.MigracionesDemo
{
    public partial class AddYearInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearsOld",
                table: "User",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearsOld",
                table: "User");
        }
    }
}
