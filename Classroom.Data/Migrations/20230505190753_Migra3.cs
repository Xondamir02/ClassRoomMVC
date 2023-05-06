using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Classroom.Data.Migrations
{
    public partial class Migra3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "UserSciences",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "UserSciences");
        }
    }
}
