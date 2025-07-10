using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class div_ssitemas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ColectorId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ColectorSfId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EnergeticoAcsId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EnergeticoCalefaccionId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EnergeticoRefrigeracionId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EquipoAcsId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EquipoCalefaccionId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EquipoRefrigeracionId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FotoTecho",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ImpSisFv",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InstTerSisFv",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "PotIns",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SistemaSolarTermico",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "SupColectores",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SupFotoTecho",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SupImptSisFv",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SupInstTerSisFv",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TempSeteoCalefaccionId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TempSeteoRefrigeracionId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TipoLuminariaId",
                table: "Divisiones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColectorId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "ColectorSfId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "EnergeticoAcsId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "EnergeticoCalefaccionId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "EnergeticoRefrigeracionId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "EquipoAcsId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "EquipoCalefaccionId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "EquipoRefrigeracionId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "FotoTecho",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "ImpSisFv",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "InstTerSisFv",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "PotIns",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "SistemaSolarTermico",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "SupColectores",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "SupFotoTecho",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "SupImptSisFv",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "SupInstTerSisFv",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "TempSeteoCalefaccionId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "TempSeteoRefrigeracionId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "TipoLuminariaId",
                table: "Divisiones");
        }
    }
}
