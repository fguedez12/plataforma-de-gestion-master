using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class ExtendCompraMedidorAddingUnidadMedida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UnidadMedidaId",
                table: "CompraMedidor",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompraMedidor_UnidadMedidaId",
                table: "CompraMedidor",
                column: "UnidadMedidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompraMedidor_UnidadesMedida_UnidadMedidaId",
                table: "CompraMedidor",
                column: "UnidadMedidaId",
                principalTable: "UnidadesMedida",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompraMedidor_UnidadesMedida_UnidadMedidaId",
                table: "CompraMedidor");

            migrationBuilder.DropIndex(
                name: "IX_CompraMedidor_UnidadMedidaId",
                table: "CompraMedidor");

            migrationBuilder.DropColumn(
                name: "UnidadMedidaId",
                table: "CompraMedidor");
        }
    }
}
