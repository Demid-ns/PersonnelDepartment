using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonnelDepartment.Migrations
{
    public partial class AvatarMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "Workers",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Workers");
        }
    }
}
