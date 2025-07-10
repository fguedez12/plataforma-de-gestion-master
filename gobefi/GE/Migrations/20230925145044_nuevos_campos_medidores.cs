using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class nuevos_campos_medidores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Chilemedido",
                table: "Medidores",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "Medidores",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MedidorConsumo",
                table: "Medidores",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chilemedido",
                table: "Medidores");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Medidores");

            migrationBuilder.DropColumn(
                name: "MedidorConsumo",
                table: "Medidores");
        }
    }
}
