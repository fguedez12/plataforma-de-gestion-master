using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class filecompromiso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdjuntoRespaldoNombreCompromiso",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdjuntoRespaldoUrlCompromiso",
                table: "Documentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdjuntoRespaldoNombreCompromiso",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "AdjuntoRespaldoUrlCompromiso",
                table: "Documentos");
        }
    }
}
