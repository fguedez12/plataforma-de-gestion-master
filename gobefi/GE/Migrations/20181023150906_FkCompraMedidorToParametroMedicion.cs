using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class FkCompraMedidorToParametroMedicion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ParametroMedicionId",
                table: "CompraMedidor",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CompraMedidor_ParametroMedicionId",
                table: "CompraMedidor",
                column: "ParametroMedicionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompraMedidor_ParametrosMedicion_ParametroMedicionId",
                table: "CompraMedidor",
                column: "ParametroMedicionId",
                principalTable: "ParametrosMedicion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompraMedidor_ParametrosMedicion_ParametroMedicionId",
                table: "CompraMedidor");

            migrationBuilder.DropIndex(
                name: "IX_CompraMedidor_ParametroMedicionId",
                table: "CompraMedidor");

            migrationBuilder.DropColumn(
                name: "ParametroMedicionId",
                table: "CompraMedidor");
        }
    }
}
