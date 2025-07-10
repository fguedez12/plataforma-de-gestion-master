using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class piso_cambio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pisos_Edificios_EdificioId",
                table: "Pisos");

            migrationBuilder.AlterColumn<long>(
                name: "EdificioId",
                table: "Pisos",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "DivisionId",
                table: "Pisos",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TipoUsoId",
                table: "Pisos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pisos_DivisionId",
                table: "Pisos",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Pisos_TipoUsoId",
                table: "Pisos",
                column: "TipoUsoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pisos_Divisiones_DivisionId",
                table: "Pisos",
                column: "DivisionId",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pisos_Edificios_EdificioId",
                table: "Pisos",
                column: "EdificioId",
                principalTable: "Edificios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pisos_TipoUsos_TipoUsoId",
                table: "Pisos",
                column: "TipoUsoId",
                principalTable: "TipoUsos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pisos_Divisiones_DivisionId",
                table: "Pisos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pisos_Edificios_EdificioId",
                table: "Pisos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pisos_TipoUsos_TipoUsoId",
                table: "Pisos");

            migrationBuilder.DropIndex(
                name: "IX_Pisos_DivisionId",
                table: "Pisos");

            migrationBuilder.DropIndex(
                name: "IX_Pisos_TipoUsoId",
                table: "Pisos");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "Pisos");

            migrationBuilder.DropColumn(
                name: "TipoUsoId",
                table: "Pisos");

            migrationBuilder.AlterColumn<long>(
                name: "EdificioId",
                table: "Pisos",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pisos_Edificios_EdificioId",
                table: "Pisos",
                column: "EdificioId",
                principalTable: "Edificios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
