using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class ColaboradoresModAlcance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColaboradoresModAlcance",
                table: "Servicios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ModificacioAlcance",
                table: "Servicios",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColaboradoresModAlcance",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "ModificacioAlcance",
                table: "Servicios");
        }
    }
}
