using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class AllowTipoUnidadEnDivisionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Divisiones_TipoUnidades_TipoUnidadId",
                table: "Divisiones");

            migrationBuilder.AlterColumn<long>(
                name: "TipoUnidadId",
                table: "Divisiones",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Divisiones_TipoUnidades_TipoUnidadId",
                table: "Divisiones",
                column: "TipoUnidadId",
                principalTable: "TipoUnidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Divisiones_TipoUnidades_TipoUnidadId",
                table: "Divisiones");

            migrationBuilder.AlterColumn<long>(
                name: "TipoUnidadId",
                table: "Divisiones",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Divisiones_TipoUnidades_TipoUnidadId",
                table: "Divisiones",
                column: "TipoUnidadId",
                principalTable: "TipoUnidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
