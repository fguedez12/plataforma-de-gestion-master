using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class no_registra_servicios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NoRegistraActividadInterna",
                table: "Servicios",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NoRegistraDifusionInterna",
                table: "Servicios",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NoRegistraDocResiduosCertificados",
                table: "Servicios",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NoRegistraDocResiduosSistemas",
                table: "Servicios",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NoRegistraPoliticaAmbiental",
                table: "Servicios",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NoRegistraProcBajaBienesMuebles",
                table: "Servicios",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NoRegistraProcComprasSustentables",
                table: "Servicios",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NoRegistraProcFormalPapel",
                table: "Servicios",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NoRegistraReutilizacionPapel",
                table: "Servicios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoRegistraActividadInterna",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "NoRegistraDifusionInterna",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "NoRegistraDocResiduosCertificados",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "NoRegistraDocResiduosSistemas",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "NoRegistraPoliticaAmbiental",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "NoRegistraProcBajaBienesMuebles",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "NoRegistraProcComprasSustentables",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "NoRegistraProcFormalPapel",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "NoRegistraReutilizacionPapel",
                table: "Servicios");
        }
    }
}
