using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class equipos_disc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AC",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CA",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FR",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AC",
                table: "TiposEquiposCalefaccion");

            migrationBuilder.DropColumn(
                name: "CA",
                table: "TiposEquiposCalefaccion");

            migrationBuilder.DropColumn(
                name: "FR",
                table: "TiposEquiposCalefaccion");
        }
    }
}
