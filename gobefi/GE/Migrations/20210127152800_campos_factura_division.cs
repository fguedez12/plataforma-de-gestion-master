using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class campos_factura_division : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccesoFactura",
                table: "Divisiones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrganizacionResponsable",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServicioResponsableId",
                table: "Divisiones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccesoFactura",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "OrganizacionResponsable",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "ServicioResponsableId",
                table: "Divisiones");
        }
    }
}
