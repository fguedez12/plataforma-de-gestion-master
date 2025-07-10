using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class obsObj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IngresoObservacionObjetivos",
                table: "DimensionServicios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ObservacionObjetivos",
                table: "DimensionServicios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IngresoObservacionObjetivos",
                table: "DimensionServicios");

            migrationBuilder.DropColumn(
                name: "ObservacionObjetivos",
                table: "DimensionServicios");
        }
    }
}
