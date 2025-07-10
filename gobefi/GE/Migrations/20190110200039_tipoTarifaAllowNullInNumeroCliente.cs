using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class tipoTarifaAllowNullInNumeroCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumeroClientes_TipoTarifas_TipoTarifaId",
                table: "NumeroClientes");

            migrationBuilder.AlterColumn<long>(
                name: "TipoTarifaId",
                table: "NumeroClientes",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_NumeroClientes_TipoTarifas_TipoTarifaId",
                table: "NumeroClientes",
                column: "TipoTarifaId",
                principalTable: "TipoTarifas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumeroClientes_TipoTarifas_TipoTarifaId",
                table: "NumeroClientes");

            migrationBuilder.AlterColumn<long>(
                name: "TipoTarifaId",
                table: "NumeroClientes",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NumeroClientes_TipoTarifas_TipoTarifaId",
                table: "NumeroClientes",
                column: "TipoTarifaId",
                principalTable: "TipoTarifas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
