using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class dimbrechaidnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medidas_DimensionBrechas_DimensionbrechaId",
                table: "Medidas");

            migrationBuilder.AlterColumn<long>(
                name: "DimensionbrechaId",
                table: "Medidas",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Medidas_DimensionBrechas_DimensionbrechaId",
                table: "Medidas",
                column: "DimensionbrechaId",
                principalTable: "DimensionBrechas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medidas_DimensionBrechas_DimensionbrechaId",
                table: "Medidas");

            migrationBuilder.AlterColumn<long>(
                name: "DimensionbrechaId",
                table: "Medidas",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Medidas_DimensionBrechas_DimensionbrechaId",
                table: "Medidas",
                column: "DimensionbrechaId",
                principalTable: "DimensionBrechas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
