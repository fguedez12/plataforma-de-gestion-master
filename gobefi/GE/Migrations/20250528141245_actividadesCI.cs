using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class actividadesCI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ActividadesCI",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropuestaTemasCI",
                table: "Documentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActividadesCI",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "PropuestaTemasCI",
                table: "Documentos");
        }
    }
}
