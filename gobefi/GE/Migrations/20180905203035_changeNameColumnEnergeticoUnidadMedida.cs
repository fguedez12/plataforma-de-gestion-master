using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class changeNameColumnEnergeticoUnidadMedida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnergeticoDivisionNClientes_EnergeticoUnidadesMedidas_EnergeticoUnMedidaId",
                table: "EnergeticoDivisionNClientes");

            migrationBuilder.RenameColumn(
                name: "EnergeticoUnMedidaId",
                table: "EnergeticoDivisionNClientes",
                newName: "EnergeticoUnidadMedidaId");

            migrationBuilder.RenameIndex(
                name: "IX_EnergeticoDivisionNClientes_EnergeticoUnMedidaId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnergeticoDivisionNClientes_EnergeticoUnidadesMedidas_EnergeticoUnidadMedidaId",
                table: "EnergeticoDivisionNClientes");

            migrationBuilder.RenameColumn(
                name: "EnergeticoUnidadMedidaId",
                table: "EnergeticoDivisionNClientes",
                newName: "EnergeticoUnMedidaId");

            migrationBuilder.RenameIndex(
                name: "IX_EnergeticoDivisionNClientes_EnergeticoUnidadMedidaId",
                table: "EnergeticoDivisionNClientes",
                newName: "IX_EnergeticoDivisionNClientes_EnergeticoUnMedidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnergeticoDivisionNClientes_EnergeticoUnidadesMedidas_EnergeticoUnMedidaId",
                table: "EnergeticoDivisionNClientes",
                column: "EnergeticoUnMedidaId",
                principalTable: "EnergeticoUnidadesMedidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
