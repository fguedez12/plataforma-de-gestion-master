using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class boolEtapa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Etapa",
                table: "TipoDocumentos");

            migrationBuilder.AddColumn<bool>(
                name: "Etapa1",
                table: "TipoDocumentos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Etapa2",
                table: "TipoDocumentos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Etapa1",
                table: "TipoDocumentos");

            migrationBuilder.DropColumn(
                name: "Etapa2",
                table: "TipoDocumentos");

            migrationBuilder.AddColumn<int>(
                name: "Etapa",
                table: "TipoDocumentos",
                nullable: true);
        }
    }
}
