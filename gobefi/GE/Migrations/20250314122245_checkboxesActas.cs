using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class checkboxesActas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DefinePropuestaConcientizados",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PuestaEnMarchaCEV",
                table: "Documentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefinePropuestaConcientizados",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "PuestaEnMarchaCEV",
                table: "Documentos");
        }
    }
}
