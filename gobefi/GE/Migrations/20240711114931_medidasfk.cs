using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class medidasfk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Medidas_DimensionbrechaId",
                table: "Medidas",
                column: "DimensionbrechaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medidas_DimensionBrechas_DimensionbrechaId",
                table: "Medidas",
                column: "DimensionbrechaId",
                principalTable: "DimensionBrechas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medidas_DimensionBrechas_DimensionbrechaId",
                table: "Medidas");

            migrationBuilder.DropIndex(
                name: "IX_Medidas_DimensionbrechaId",
                table: "Medidas");
        }
    }
}
