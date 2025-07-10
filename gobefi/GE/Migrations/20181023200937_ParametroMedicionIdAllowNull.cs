using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class ParametroMedicionIdAllowNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompraMedidor_ParametrosMedicion_ParametroMedicionId",
                table: "CompraMedidor");

            migrationBuilder.AlterColumn<long>(
                name: "ParametroMedicionId",
                table: "CompraMedidor",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_CompraMedidor_ParametrosMedicion_ParametroMedicionId",
                table: "CompraMedidor",
                column: "ParametroMedicionId",
                principalTable: "ParametrosMedicion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompraMedidor_ParametrosMedicion_ParametroMedicionId",
                table: "CompraMedidor");

            migrationBuilder.AlterColumn<long>(
                name: "ParametroMedicionId",
                table: "CompraMedidor",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompraMedidor_ParametrosMedicion_ParametroMedicionId",
                table: "CompraMedidor",
                column: "ParametroMedicionId",
                principalTable: "ParametrosMedicion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
