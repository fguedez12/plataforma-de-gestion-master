using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class responsable_accion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponsableEmail",
                table: "Acciones",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsableNombre",
                table: "Acciones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponsableEmail",
                table: "Acciones");

            migrationBuilder.DropColumn(
                name: "ResponsableNombre",
                table: "Acciones");
        }
    }
}
