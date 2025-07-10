using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class mod_compras_sust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdjuntoNombreCompraFuera",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdjuntoUrlCompraFuera",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NComprasCriterios2",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NComprasRubros2",
                table: "Documentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdjuntoNombreCompraFuera",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "AdjuntoUrlCompraFuera",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "NComprasCriterios2",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "NComprasRubros2",
                table: "Documentos");
        }
    }
}
