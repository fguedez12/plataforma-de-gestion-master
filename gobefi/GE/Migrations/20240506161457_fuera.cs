using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class fuera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NComprasCriteriosFuera",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NComprasRubrosFuera",
                table: "Documentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NComprasCriteriosFuera",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "NComprasRubrosFuera",
                table: "Documentos");
        }
    }
}
