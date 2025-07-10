using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class strings_menuv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "ActiveV3",
                table: "Menu",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<string>(
                name: "Component",
                table: "Menu",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Folder",
                table: "Menu",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Component",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "Folder",
                table: "Menu");

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveV3",
                table: "Menu",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
