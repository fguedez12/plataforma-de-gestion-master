using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class medidanull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acciones_Medidas_MedidaId",
                table: "Acciones");

            migrationBuilder.AlterColumn<long>(
                name: "MedidaId",
                table: "Acciones",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Acciones_Medidas_MedidaId",
                table: "Acciones",
                column: "MedidaId",
                principalTable: "Medidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acciones_Medidas_MedidaId",
                table: "Acciones");

            migrationBuilder.AlterColumn<long>(
                name: "MedidaId",
                table: "Acciones",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Acciones_Medidas_MedidaId",
                table: "Acciones",
                column: "MedidaId",
                principalTable: "Medidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
