using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class campos_pga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PgaObservacionRed",
                table: "Servicios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PgaRespuestaRed",
                table: "Servicios",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PgaRevisionRed",
                table: "Servicios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PgaObservacionRed",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "PgaRespuestaRed",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "PgaRevisionRed",
                table: "Servicios");
        }
    }
}
