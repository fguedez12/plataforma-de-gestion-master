using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class ExtenderTablaSubMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "SubMenu",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Controller",
                table: "SubMenu",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icono",
                table: "SubMenu",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "SubMenu",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "SubMenu");

            migrationBuilder.DropColumn(
                name: "Controller",
                table: "SubMenu");

            migrationBuilder.DropColumn(
                name: "Icono",
                table: "SubMenu");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "SubMenu");
        }
    }
}
