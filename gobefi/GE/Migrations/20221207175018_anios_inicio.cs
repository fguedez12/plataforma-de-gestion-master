using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class anios_inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnioInicioGestionEnergetica",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnioInicioRestoItems",
                table: "Divisiones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnioInicioGestionEnergetica",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "AnioInicioRestoItems",
                table: "Divisiones");
        }
    }
}
