using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class obsindicadores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IngresoObservacionIndicadores",
                table: "DimensionServicios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ObservacionIndicadores",
                table: "DimensionServicios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IngresoObservacionIndicadores",
                table: "DimensionServicios");

            migrationBuilder.DropColumn(
                name: "ObservacionIndicadores",
                table: "DimensionServicios");
        }
    }
}
