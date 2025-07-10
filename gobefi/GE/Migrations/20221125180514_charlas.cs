using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class charlas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentoPadreId",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NParticipantes",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoAdjunto",
                table: "Documentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentoPadreId",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "NParticipantes",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "TipoAdjunto",
                table: "Documentos");
        }
    }
}
