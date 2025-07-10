using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class adjuntorespaldo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdjuntoRespaldoNombre",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdjuntoRespaldoUrl",
                table: "Documentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdjuntoRespaldoNombre",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "AdjuntoRespaldoUrl",
                table: "Documentos");
        }
    }
}
