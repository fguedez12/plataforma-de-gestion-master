using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class posicionventana_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventanas_PosicionVentanas_PosicionVentanaId",
                table: "Ventanas");

            migrationBuilder.AlterColumn<long>(
                name: "PosicionVentanaId",
                table: "Ventanas",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Ventanas_PosicionVentanas_PosicionVentanaId",
                table: "Ventanas",
                column: "PosicionVentanaId",
                principalTable: "PosicionVentanas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventanas_PosicionVentanas_PosicionVentanaId",
                table: "Ventanas");

            migrationBuilder.AlterColumn<long>(
                name: "PosicionVentanaId",
                table: "Ventanas",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventanas_PosicionVentanas_PosicionVentanaId",
                table: "Ventanas",
                column: "PosicionVentanaId",
                principalTable: "PosicionVentanas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
