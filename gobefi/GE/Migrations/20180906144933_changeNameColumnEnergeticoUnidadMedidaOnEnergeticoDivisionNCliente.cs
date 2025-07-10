using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class changeNameColumnEnergeticoUnidadMedidaOnEnergeticoDivisionNCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnergeticoDivisionNClientes_EnergeticoUnidadesMedidas_EnergeticoUnidadMedidaId",
                table: "EnergeticoDivisionNClientes");

            migrationBuilder.RenameColumn(
                name: "EnergeticoUnidadMedidaId",
                table: "EnergeticoDivisionNClientes",
                newName: "UnidadMedidaId");

            migrationBuilder.RenameIndex(
                name: "IX_EnergeticoDivisionNClientes_EnergeticoUnidadMedidaId",
                table: "EnergeticoDivisionNClientes",
                newName: "IX_EnergeticoDivisionNClientes_UnidadMedidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnergeticoDivisionNClientes_UnidadesMedidas_UnidadMedidaId",
                table: "EnergeticoDivisionNClientes",
                column: "UnidadMedidaId",
                principalTable: "UnidadesMedidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnergeticoDivisionNClientes_UnidadesMedidas_UnidadMedidaId",
                table: "EnergeticoDivisionNClientes");

            migrationBuilder.RenameColumn(
                name: "UnidadMedidaId",
                table: "EnergeticoDivisionNClientes",
                newName: "EnergeticoUnidadMedidaId");

            migrationBuilder.RenameIndex(
                name: "IX_EnergeticoDivisionNClientes_UnidadMedidaId",
                table: "EnergeticoDivisionNClientes",
                newName: "IX_EnergeticoDivisionNClientes_EnergeticoUnidadMedidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnergeticoDivisionNClientes_EnergeticoUnidadesMedidas_EnergeticoUnidadMedidaId",
                table: "EnergeticoDivisionNClientes",
                column: "EnergeticoUnidadMedidaId",
                principalTable: "EnergeticoUnidadesMedidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
