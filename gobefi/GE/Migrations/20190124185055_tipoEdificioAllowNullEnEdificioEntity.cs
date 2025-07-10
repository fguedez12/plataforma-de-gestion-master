using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class tipoEdificioAllowNullEnEdificioEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Edificios_TipoEdificios_TipoEdificioId",
                table: "Edificios");

            migrationBuilder.AlterColumn<long>(
                name: "TipoEdificioId",
                table: "Edificios",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Edificios_TipoEdificios_TipoEdificioId",
                table: "Edificios",
                column: "TipoEdificioId",
                principalTable: "TipoEdificios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Edificios_TipoEdificios_TipoEdificioId",
                table: "Edificios");

            migrationBuilder.AlterColumn<long>(
                name: "TipoEdificioId",
                table: "Edificios",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Edificios_TipoEdificios_TipoEdificioId",
                table: "Edificios",
                column: "TipoEdificioId",
                principalTable: "TipoEdificios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
