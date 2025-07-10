using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class ancho_largo_ventanas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Ancho",
                table: "Ventanas",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Largo",
                table: "Ventanas",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Ventanas",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ancho",
                table: "Ventanas");

            migrationBuilder.DropColumn(
                name: "Largo",
                table: "Ventanas");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Ventanas");
        }
    }
}
