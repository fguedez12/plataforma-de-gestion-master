using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class correccionnombre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdjuntoNombreComraSustentableAnt",
                table: "Documentos",
                newName: "AdjuntoNombreCompraSustentableAnt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdjuntoNombreCompraSustentableAnt",
                table: "Documentos",
                newName: "AdjuntoNombreComraSustentableAnt");
        }
    }
}
