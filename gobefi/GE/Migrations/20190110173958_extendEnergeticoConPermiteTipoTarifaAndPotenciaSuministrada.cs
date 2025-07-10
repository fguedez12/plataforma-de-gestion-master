using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class extendEnergeticoConPermiteTipoTarifaAndPotenciaSuministrada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PermitePotenciaSuministrada",
                table: "Energeticos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PermiteTipoTarifa",
                table: "Energeticos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermitePotenciaSuministrada",
                table: "Energeticos");

            migrationBuilder.DropColumn(
                name: "PermiteTipoTarifa",
                table: "Energeticos");
        }
    }
}
