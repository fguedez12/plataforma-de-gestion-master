using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class checkpolitica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ActualizaPolitica",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ElaboraPolitica",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MantienePolitica",
                table: "Documentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualizaPolitica",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "ElaboraPolitica",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "MantienePolitica",
                table: "Documentos");
        }
    }
}
