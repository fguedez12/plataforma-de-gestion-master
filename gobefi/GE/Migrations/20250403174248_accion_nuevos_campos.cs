using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class accion_nuevos_campos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemPresupuestario",
                table: "Acciones",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Subtitulo",
                table: "Acciones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemPresupuestario",
                table: "Acciones");

            migrationBuilder.DropColumn(
                name: "Subtitulo",
                table: "Acciones");
        }
    }
}
