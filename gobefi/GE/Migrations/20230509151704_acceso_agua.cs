using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class acceso_agua : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccesoFacturaAgua",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstitucionResponsableAguaId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizacionResponsableAgua",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServicioResponsableAguaId",
                table: "Divisiones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccesoFacturaAgua",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "InstitucionResponsableAguaId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "OrganizacionResponsableAgua",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "ServicioResponsableAguaId",
                table: "Divisiones");
        }
    }
}
