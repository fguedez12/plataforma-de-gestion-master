using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class calefa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AireAcondicionadoElectricidad",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CalefaccionGas",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DisponeCalefaccion",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AireAcondicionadoElectricidad",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "CalefaccionGas",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "DisponeCalefaccion",
                table: "Divisiones");
        }
    }
}
