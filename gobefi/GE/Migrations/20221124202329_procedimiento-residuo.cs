using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class procedimientoresiduo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EntregaCertificadoRegistroCantidades",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EntregaCertificadoRegistroDisposicion",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EntregaCertificadoRegistroRetiro",
                table: "Documentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntregaCertificadoRegistroCantidades",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "EntregaCertificadoRegistroDisposicion",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "EntregaCertificadoRegistroRetiro",
                table: "Documentos");
        }
    }
}
