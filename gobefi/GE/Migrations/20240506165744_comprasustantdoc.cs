using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class comprasustantdoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdjuntoNombreComraSustentableAnt",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdjuntoUrlCompraSustentableAnt",
                table: "Documentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdjuntoNombreComraSustentableAnt",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "AdjuntoUrlCompraSustentableAnt",
                table: "Documentos");
        }
    }
}
