using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class checks_reuniones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ApruebaAlcanceGradualSEV",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ApruebaDiagnostico",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DetActDeConcientizacion",
                table: "Documentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApruebaAlcanceGradualSEV",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "ApruebaDiagnostico",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "DetActDeConcientizacion",
                table: "Documentos");
        }
    }
}
