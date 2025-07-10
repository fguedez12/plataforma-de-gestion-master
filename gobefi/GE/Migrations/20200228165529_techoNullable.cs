using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class techoNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Techos_Aislaciones_AislacionId",
                table: "Techos");

            migrationBuilder.DropForeignKey(
                name: "FK_Techos_Materialidades_MaterialidadId",
                table: "Techos");

            migrationBuilder.AlterColumn<long>(
                name: "MaterialidadId",
                table: "Techos",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "AislacionId",
                table: "Techos",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Techos_Aislaciones_AislacionId",
                table: "Techos",
                column: "AislacionId",
                principalTable: "Aislaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Techos_Materialidades_MaterialidadId",
                table: "Techos",
                column: "MaterialidadId",
                principalTable: "Materialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Techos_Aislaciones_AislacionId",
                table: "Techos");

            migrationBuilder.DropForeignKey(
                name: "FK_Techos_Materialidades_MaterialidadId",
                table: "Techos");

            migrationBuilder.AlterColumn<long>(
                name: "MaterialidadId",
                table: "Techos",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AislacionId",
                table: "Techos",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Techos_Aislaciones_AislacionId",
                table: "Techos",
                column: "AislacionId",
                principalTable: "Aislaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Techos_Materialidades_MaterialidadId",
                table: "Techos",
                column: "MaterialidadId",
                principalTable: "Materialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
