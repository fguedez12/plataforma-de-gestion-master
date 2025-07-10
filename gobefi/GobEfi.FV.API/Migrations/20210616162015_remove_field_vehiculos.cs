using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.FV.API.Migrations
{
    public partial class remove_field_vehiculos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "Propulsion",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "TipoVehiculo",
                table: "Vehiculos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Propulsion",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoVehiculo",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
