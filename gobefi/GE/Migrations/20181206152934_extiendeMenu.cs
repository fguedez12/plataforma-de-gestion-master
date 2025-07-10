using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class extiendeMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icono",
                table: "Menu",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdTag",
                table: "Menu",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icono",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "IdTag",
                table: "Menu");
        }
    }
}
