using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class disponevehiculo_null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "DisponeVehiculo",
                table: "Divisiones",
                nullable: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "DisponeVehiculo",
                table: "Divisiones",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
