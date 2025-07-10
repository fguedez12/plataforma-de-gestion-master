using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class ExtenderEntidadesReporteAndTipoArchivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "TipoArchivos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GenerarData",
                table: "Reportes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RutaDondeObtenerArchivo",
                table: "Reportes",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TipoArchivoId",
                table: "Reportes",
                nullable: false,
                defaultValue: 4L);
            //defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_TipoArchivoId",
                table: "Reportes",
                column: "TipoArchivoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reportes_TipoArchivos_TipoArchivoId",
                table: "Reportes",
                column: "TipoArchivoId",
                principalTable: "TipoArchivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reportes_TipoArchivos_TipoArchivoId",
                table: "Reportes");

            migrationBuilder.DropIndex(
                name: "IX_Reportes_TipoArchivoId",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "TipoArchivos");

            migrationBuilder.DropColumn(
                name: "GenerarData",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "RutaDondeObtenerArchivo",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TipoArchivoId",
                table: "Reportes");
        }
    }
}
