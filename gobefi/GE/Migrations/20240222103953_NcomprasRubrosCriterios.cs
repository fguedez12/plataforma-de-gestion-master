using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class NcomprasRubrosCriterios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NComprasCriterios",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NComprasRubros",
                table: "Documentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NComprasCriterios",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "NComprasRubros",
                table: "Documentos");
        }
    }
}
