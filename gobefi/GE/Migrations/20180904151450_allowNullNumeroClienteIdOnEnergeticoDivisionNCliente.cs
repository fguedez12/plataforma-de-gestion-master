using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class allowNullNumeroClienteIdOnEnergeticoDivisionNCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnergeticoDivisionNClientes_NumeroClientes_NumeroClienteId",
                table: "EnergeticoDivisionNClientes");

            migrationBuilder.DropIndex(
                name: "IX_EnergeticoDivisionNClientes_NumeroClienteId",
                table: "EnergeticoDivisionNClientes");

            migrationBuilder.AlterColumn<long>(
                name: "NumeroClienteId",
                table: "EnergeticoDivisionNClientes",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "NumeroClienteId",
                table: "EnergeticoDivisionNClientes",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnergeticoDivisionNClientes_NumeroClienteId",
                table: "EnergeticoDivisionNClientes",
                column: "NumeroClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnergeticoDivisionNClientes_NumeroClientes_NumeroClienteId",
                table: "EnergeticoDivisionNClientes",
                column: "NumeroClienteId",
                principalTable: "NumeroClientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
