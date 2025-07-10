using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class addUnidadMedidaEnCompras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UnidadMedidaId",
                table: "Compras",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compras_UnidadMedidaId",
                table: "Compras",
                column: "UnidadMedidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_UnidadesMedida_UnidadMedidaId",
                table: "Compras",
                column: "UnidadMedidaId",
                principalTable: "UnidadesMedida",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_UnidadesMedida_UnidadMedidaId",
                table: "Compras");

            migrationBuilder.DropIndex(
                name: "IX_Compras_UnidadMedidaId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "UnidadMedidaId",
                table: "Compras");
        }
    }
}
