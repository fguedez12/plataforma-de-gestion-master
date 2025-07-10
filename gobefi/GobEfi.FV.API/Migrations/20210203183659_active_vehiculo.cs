using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.FV.API.Migrations
{
    public partial class active_vehiculo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Vehiculos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Vehiculos");
        }
    }
}
