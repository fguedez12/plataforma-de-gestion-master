using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class decimal_to_latlng_precition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitud",
                table: "Muros",
                type: "decimal(18,15)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitud",
                table: "Muros",
                type: "decimal(18,15)",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitud",
                table: "Muros",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,15)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitud",
                table: "Muros",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,15)");
        }
    }
}
