using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class FkCompraArchivoAdjunto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivoAdjuntos_Compras_CompraId",
                table: "ArchivoAdjuntos");

            migrationBuilder.DropIndex(
                name: "IX_ArchivoAdjuntos_CompraId",
                table: "ArchivoAdjuntos");

            migrationBuilder.DropColumn(
                name: "CompraId",
                table: "ArchivoAdjuntos");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_FacturaId",
                table: "Compras",
                column: "FacturaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_ArchivoAdjuntos_FacturaId",
                table: "Compras",
                column: "FacturaId",
                principalTable: "ArchivoAdjuntos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_ArchivoAdjuntos_FacturaId",
                table: "Compras");

            migrationBuilder.DropIndex(
                name: "IX_Compras_FacturaId",
                table: "Compras");

            migrationBuilder.AddColumn<long>(
                name: "CompraId",
                table: "ArchivoAdjuntos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArchivoAdjuntos_CompraId",
                table: "ArchivoAdjuntos",
                column: "CompraId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivoAdjuntos_Compras_CompraId",
                table: "ArchivoAdjuntos",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
