using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class AllowNullEmpresaDistribuidoraEnNumeroCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumeroClientes_EmpresaDistribuidoras_EmpresaDistribuidoraId",
                table: "NumeroClientes");

            migrationBuilder.AlterColumn<long>(
                name: "EmpresaDistribuidoraId",
                table: "NumeroClientes",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_NumeroClientes_EmpresaDistribuidoras_EmpresaDistribuidoraId",
                table: "NumeroClientes",
                column: "EmpresaDistribuidoraId",
                principalTable: "EmpresaDistribuidoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumeroClientes_EmpresaDistribuidoras_EmpresaDistribuidoraId",
                table: "NumeroClientes");

            migrationBuilder.AlterColumn<long>(
                name: "EmpresaDistribuidoraId",
                table: "NumeroClientes",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NumeroClientes_EmpresaDistribuidoras_EmpresaDistribuidoraId",
                table: "NumeroClientes",
                column: "EmpresaDistribuidoraId",
                principalTable: "EmpresaDistribuidoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
