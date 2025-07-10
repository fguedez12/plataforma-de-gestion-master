using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class nobree2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombreE2",
                table: "TipoDocumentos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "etapa",
                table: "TipoDocumentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreE2",
                table: "TipoDocumentos");

            migrationBuilder.DropColumn(
                name: "etapa",
                table: "TipoDocumentos");
        }
    }
}
