using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.FV.API.Migrations
{
    public partial class region_comuna_campos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ComunaId",
                table: "Vehiculos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RegionId",
                table: "Vehiculos",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComunaId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "Vehiculos");
        }
    }
}
