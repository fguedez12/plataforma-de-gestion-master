using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class tipoAgrupamientoAllowNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Divisiones_TipoAgrupamientos_TipoAgrupamientoId",
                table: "Divisiones");

            migrationBuilder.AlterColumn<long>(
                name: "TipoAgrupamientoId",
                table: "Divisiones",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Divisiones_TipoAgrupamientos_TipoAgrupamientoId",
                table: "Divisiones",
                column: "TipoAgrupamientoId",
                principalTable: "TipoAgrupamientos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Divisiones_TipoAgrupamientos_TipoAgrupamientoId",
                table: "Divisiones");

            migrationBuilder.AlterColumn<long>(
                name: "TipoAgrupamientoId",
                table: "Divisiones",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Divisiones_TipoAgrupamientos_TipoAgrupamientoId",
                table: "Divisiones",
                column: "TipoAgrupamientoId",
                principalTable: "TipoAgrupamientos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
