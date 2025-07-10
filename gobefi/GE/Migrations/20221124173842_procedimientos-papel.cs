using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class procedimientospapel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DifusionInterna",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Implementacion",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ImpresionDobleCara",
                table: "Documentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DifusionInterna",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "Implementacion",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "ImpresionDobleCara",
                table: "Documentos");
        }
    }
}
