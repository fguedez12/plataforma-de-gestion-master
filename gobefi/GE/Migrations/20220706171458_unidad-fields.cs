using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class unidadfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccesoFactura",
                table: "Unidades",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Funcionarios",
                table: "Unidades",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IndicadorEE",
                table: "Unidades",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "InstitucionResponsableId",
                table: "Unidades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizacionResponsable",
                table: "Unidades",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReportaPMG",
                table: "Unidades",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ServicioResponsableId",
                table: "Unidades",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccesoFactura",
                table: "Unidades");

            migrationBuilder.DropColumn(
                name: "Funcionarios",
                table: "Unidades");

            migrationBuilder.DropColumn(
                name: "IndicadorEE",
                table: "Unidades");

            migrationBuilder.DropColumn(
                name: "InstitucionResponsableId",
                table: "Unidades");

            migrationBuilder.DropColumn(
                name: "OrganizacionResponsable",
                table: "Unidades");

            migrationBuilder.DropColumn(
                name: "ReportaPMG",
                table: "Unidades");

            migrationBuilder.DropColumn(
                name: "ServicioResponsableId",
                table: "Unidades");
        }
    }
}
