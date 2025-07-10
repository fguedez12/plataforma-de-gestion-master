using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class ExtenderConControllerAdemasDelActionParaMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActionName",
                table: "Menu",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ControllerName",
                table: "Menu",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Menu",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionName",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "ControllerName",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Menu");
        }
    }
}
