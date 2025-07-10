using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class vehiculos_division : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TieneVehiculo",
                table: "Divisiones",
                newName: "DisponeVehiculo");

            migrationBuilder.AddColumn<string>(
                name: "VehiculosIds",
                table: "Divisiones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehiculosIds",
                table: "Divisiones");

            migrationBuilder.RenameColumn(
                name: "DisponeVehiculo",
                table: "Divisiones",
                newName: "TieneVehiculo");
        }
    }
}
