using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class SeExtiendeDivision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ComparteMedidorElectricidad",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ComparteMedidorGasCanieria",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ReportaIndicadorEfiEnergetica",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComparteMedidorElectricidad",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "ComparteMedidorGasCanieria",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "ReportaIndicadorEfiEnergetica",
                table: "Divisiones");
        }
    }
}
