using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class add_foreign_keys_compras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_AspNetUsers_UsuarioId1",
                table: "Compras");

            migrationBuilder.DropForeignKey(
                name: "FK_Medidores_NumeroClientes_NumeroClienteId",
                table: "Medidores");

            migrationBuilder.DropIndex(
                name: "IX_Compras_UsuarioId1",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Compras");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Compras",
                newName: "DivisionId");

            migrationBuilder.AddColumn<long>(
                name: "DivisionId",
                table: "NumeroClientes",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "NumeroClienteId",
                table: "Medidores",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DivisionId1",
                table: "Medidores",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NumeroClientes_DivisionId",
                table: "NumeroClientes",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Medidores_DivisionId1",
                table: "Medidores",
                column: "DivisionId1");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_DivisionId",
                table: "Compras",
                column: "DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Divisiones_DivisionId",
                table: "Compras",
                column: "DivisionId",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medidores_Divisiones_DivisionId1",
                table: "Medidores",
                column: "DivisionId1",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medidores_NumeroClientes_NumeroClienteId",
                table: "Medidores",
                column: "NumeroClienteId",
                principalTable: "NumeroClientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NumeroClientes_Divisiones_DivisionId",
                table: "NumeroClientes",
                column: "DivisionId",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Divisiones_DivisionId",
                table: "Compras");

            migrationBuilder.DropForeignKey(
                name: "FK_Medidores_Divisiones_DivisionId1",
                table: "Medidores");

            migrationBuilder.DropForeignKey(
                name: "FK_Medidores_NumeroClientes_NumeroClienteId",
                table: "Medidores");

            migrationBuilder.DropForeignKey(
                name: "FK_NumeroClientes_Divisiones_DivisionId",
                table: "NumeroClientes");

            migrationBuilder.DropIndex(
                name: "IX_NumeroClientes_DivisionId",
                table: "NumeroClientes");

            migrationBuilder.DropIndex(
                name: "IX_Medidores_DivisionId1",
                table: "Medidores");

            migrationBuilder.DropIndex(
                name: "IX_Compras_DivisionId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "NumeroClientes");

            migrationBuilder.DropColumn(
                name: "DivisionId1",
                table: "Medidores");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "Compras",
                newName: "UsuarioId");

            migrationBuilder.AlterColumn<long>(
                name: "NumeroClienteId",
                table: "Medidores",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Compras",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compras_UsuarioId1",
                table: "Compras",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_AspNetUsers_UsuarioId1",
                table: "Compras",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medidores_NumeroClientes_NumeroClienteId",
                table: "Medidores",
                column: "NumeroClienteId",
                principalTable: "NumeroClientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
