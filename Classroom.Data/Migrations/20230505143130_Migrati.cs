using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Classroom.Data.Migrations
{
    public partial class Migrati : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "science",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "science",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "science",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "science",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "science",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.CreateTable(
                name: "UserSciences",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSciences", x => new { x.UserId, x.ScienceId });
                    table.ForeignKey(
                        name: "FK_UserSciences_science_ScienceId",
                        column: x => x.ScienceId,
                        principalTable: "science",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSciences_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSciences_ScienceId",
                table: "UserSciences",
                column: "ScienceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSciences");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "science",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "science",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "science",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "science",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "science",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
