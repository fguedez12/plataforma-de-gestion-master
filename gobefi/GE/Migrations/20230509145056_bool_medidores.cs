using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class bool_medidores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TieneMedidorElectricidad",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TieneMedidorGas",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TieneMedidorElectricidad",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "TieneMedidorGas",
                table: "Divisiones");
        }
    }
}
