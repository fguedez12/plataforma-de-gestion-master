using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class checkboxescharlas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CambioClimatico",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GestionAgua",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GestionBajaBs",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GestionComprasS",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GestionEnergia",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GestionResiduosEc",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GestionVehiculosTs",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HuellaC",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MateriaGestionPapel",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OtraMateria",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TrasladoSustentable",
                table: "Documentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CambioClimatico",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "GestionAgua",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "GestionBajaBs",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "GestionComprasS",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "GestionEnergia",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "GestionResiduosEc",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "GestionVehiculosTs",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "HuellaC",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "MateriaGestionPapel",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "OtraMateria",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "TrasladoSustentable",
                table: "Documentos");
        }
    }
}
