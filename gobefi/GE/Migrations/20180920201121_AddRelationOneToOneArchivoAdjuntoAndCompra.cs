using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class AddRelationOneToOneArchivoAdjuntoAndCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacturaUrl",
                table: "Compras");

            migrationBuilder.AddColumn<long>(
                name: "CompraId",
                table: "ArchivoAdjuntos",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ArchivoAdjuntos_CompraId",
                table: "ArchivoAdjuntos",
                column: "CompraId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivoAdjuntos_Compras_CompraId",
                table: "ArchivoAdjuntos",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "FacturaUrl",
                table: "Compras",
                nullable: true);
        }
    }
}
