using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class CrearIndexAndLlaveForaneaEnMedidorDivision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MedidorDivision_DivisionId",
                table: "MedidorDivision",
                column: "DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedidorDivision_Divisiones_DivisionId",
                table: "MedidorDivision",
                column: "DivisionId",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedidorDivision_Divisiones_DivisionId",
                table: "MedidorDivision");

            migrationBuilder.DropIndex(
                name: "IX_MedidorDivision_DivisionId",
                table: "MedidorDivision");
        }
    }
}
