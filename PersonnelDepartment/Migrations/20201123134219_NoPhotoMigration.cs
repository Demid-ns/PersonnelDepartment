using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonnelDepartment.Migrations
{
    public partial class NoPhotoMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Workers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
