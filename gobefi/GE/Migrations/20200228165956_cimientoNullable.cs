using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class cimientoNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cimientos_Materialidades_MaterialidadId",
                table: "Cimientos");

            migrationBuilder.AlterColumn<long>(
                name: "MaterialidadId",
                table: "Cimientos",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Cimientos_Materialidades_MaterialidadId",
                table: "Cimientos",
                column: "MaterialidadId",
                principalTable: "Materialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cimientos_Materialidades_MaterialidadId",
                table: "Cimientos");

            migrationBuilder.AlterColumn<long>(
                name: "MaterialidadId",
                table: "Cimientos",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cimientos_Materialidades_MaterialidadId",
                table: "Cimientos",
                column: "MaterialidadId",
                principalTable: "Materialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
