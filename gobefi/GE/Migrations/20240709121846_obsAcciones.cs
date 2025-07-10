using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class obsAcciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IngresoObservacionAcciones",
                table: "DimensionServicios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ObservacionAcciones",
                table: "DimensionServicios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IngresoObservacionAcciones",
                table: "DimensionServicios");

            migrationBuilder.DropColumn(
                name: "ObservacionAcciones",
                table: "DimensionServicios");
        }
    }
}
