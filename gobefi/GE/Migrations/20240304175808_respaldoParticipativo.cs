using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class respaldoParticipativo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdjuntoRespaldoNombreParticipativo",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdjuntoRespaldoUrlParticipativo",
                table: "Documentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdjuntoRespaldoNombreParticipativo",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "AdjuntoRespaldoUrlParticipativo",
                table: "Documentos");
        }
    }
}
