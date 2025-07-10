using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class numerocliente_potencia_decimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PotenciaSuministrada",
                table: "NumeroClientes",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PotenciaSuministrada",
                table: "NumeroClientes",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
