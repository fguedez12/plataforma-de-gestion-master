using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class NuevosCamposParaReporte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProcedimientoAlmacenado",
                table: "Reportes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SeGeneraAutomatico",
                table: "Reportes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcedimientoAlmacenado",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "SeGeneraAutomatico",
                table: "Reportes");
        }
    }
}
