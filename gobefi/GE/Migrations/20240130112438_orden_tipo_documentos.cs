using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class orden_tipo_documentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Orden",
                table: "TipoDocumentos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Orden",
                table: "TipoDocumentos");
        }
    }
}
