using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class politicas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cobertura",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EstandaresSustentabilidad",
                table: "Documentos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProcesoGestionSustentable",
                table: "Documentos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProdBajoImpactoAmbiental",
                table: "Documentos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Reciclaje",
                table: "Documentos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Reduccion",
                table: "Documentos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Reutilizacion",
                table: "Documentos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ComprasSustentables",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EficienciaEnergetica",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GestionPapel",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Otras",
                table: "Documentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cobertura",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "EstandaresSustentabilidad",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "ProcesoGestionSustentable",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "ProdBajoImpactoAmbiental",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "Reciclaje",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "Reduccion",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "Reutilizacion",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "ComprasSustentables",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "EficienciaEnergetica",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "GestionPapel",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "Otras",
                table: "Documentos");
        }
    }
}
