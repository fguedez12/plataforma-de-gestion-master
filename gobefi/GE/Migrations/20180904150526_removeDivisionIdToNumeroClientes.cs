using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class removeDivisionIdToNumeroClientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumeroClientes_Divisiones_DivisionId",
                table: "NumeroClientes");

            migrationBuilder.DropIndex(
                name: "IX_NumeroClientes_DivisionId",
                table: "NumeroClientes");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "NumeroClientes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
