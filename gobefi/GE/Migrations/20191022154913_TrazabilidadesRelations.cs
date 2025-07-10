using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class TrazabilidadesRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Trazabilidades_DivisionId",
                table: "Trazabilidades",
                column: "DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trazabilidades_Divisiones_DivisionId",
                table: "Trazabilidades",
                column: "DivisionId",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trazabilidades_Divisiones_DivisionId",
                table: "Trazabilidades");

            migrationBuilder.DropIndex(
                name: "IX_Trazabilidades_DivisionId",
                table: "Trazabilidades");
        }
    }
}
