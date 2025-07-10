using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class ModifcaMedidoresAndEnergeticoCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnergeticoCliente_NumeroClientes_ClienteId",
                table: "EnergeticoCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_Medidores_Divisiones_DivisionId1",
                table: "Medidores");

            migrationBuilder.DropForeignKey(
                name: "FK_Medidores_EnergeticoDivisiones_EnergeticoDivisionId",
                table: "Medidores");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "EnergeticoCliente",
                newName: "NumClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_EnergeticoCliente_ClienteId",
                table: "EnergeticoCliente",
                newName: "IX_EnergeticoCliente_NumClienteId");

            migrationBuilder.AlterColumn<long>(
                name: "EnergeticoDivisionId",
                table: "Medidores",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "DivisionId",
                table: "Medidores",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_EnergeticoCliente_NumeroClientes_NumClienteId",
                table: "EnergeticoCliente",
                column: "NumClienteId",
                principalTable: "NumeroClientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medidores_Divisiones_DivisionId",
                table: "Medidores",
                column: "DivisionId",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medidores_EnergeticoDivisiones_EnergeticoDivisionId",
                table: "Medidores",
                column: "EnergeticoDivisionId",
                principalTable: "EnergeticoDivisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnergeticoCliente_NumeroClientes_NumClienteId",
                table: "EnergeticoCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_Medidores_Divisiones_DivisionId",
                table: "Medidores");

            migrationBuilder.DropForeignKey(
                name: "FK_Medidores_EnergeticoDivisiones_EnergeticoDivisionId",
                table: "Medidores");

            migrationBuilder.RenameColumn(
                name: "NumClienteId",
                table: "EnergeticoCliente",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_EnergeticoCliente_NumClienteId",
                table: "EnergeticoCliente",
                newName: "IX_EnergeticoCliente_ClienteId");

            migrationBuilder.AlterColumn<long>(
                name: "EnergeticoDivisionId",
                table: "Medidores",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DivisionId",
                table: "Medidores",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EnergeticoCliente_NumeroClientes_ClienteId",
                table: "EnergeticoCliente",
                column: "ClienteId",
                principalTable: "NumeroClientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medidores_Divisiones_DivisionId",
                table: "Medidores",
                column: "DivisionId",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medidores_EnergeticoDivisiones_EnergeticoDivisionId",
                table: "Medidores",
                column: "EnergeticoDivisionId",
                principalTable: "EnergeticoDivisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
