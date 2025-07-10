using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class addFkNCtaTipoTarifa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TipoTarifaId",
                table: "NumeroClientes",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_NumeroClientes_TipoTarifaId",
                table: "NumeroClientes",
                column: "TipoTarifaId");

            migrationBuilder.AddForeignKey(
                name: "FK_NumeroClientes_TipoTarifas_TipoTarifaId",
                table: "NumeroClientes",
                column: "TipoTarifaId",
                principalTable: "TipoTarifas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumeroClientes_TipoTarifas_TipoTarifaId",
                table: "NumeroClientes");

            migrationBuilder.DropIndex(
                name: "IX_NumeroClientes_TipoTarifaId",
                table: "NumeroClientes");

            migrationBuilder.DropColumn(
                name: "TipoTarifaId",
                table: "NumeroClientes");
        }
    }
}
