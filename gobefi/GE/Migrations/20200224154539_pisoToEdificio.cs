using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class pisoToEdificio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pisos_Divisiones_DivisionId",
                table: "Pisos");

            migrationBuilder.DropColumn(
                name: "FrontisIndex",
                table: "Pisos");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "Pisos",
                newName: "EdificioId");

            migrationBuilder.RenameIndex(
                name: "IX_Pisos_DivisionId",
                table: "Pisos",
                newName: "IX_Pisos_EdificioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pisos_Edificios_EdificioId",
                table: "Pisos",
                column: "EdificioId",
                principalTable: "Edificios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pisos_Edificios_EdificioId",
                table: "Pisos");

            migrationBuilder.RenameColumn(
                name: "EdificioId",
                table: "Pisos",
                newName: "DivisionId");

            migrationBuilder.RenameIndex(
                name: "IX_Pisos_EdificioId",
                table: "Pisos",
                newName: "IX_Pisos_DivisionId");

            migrationBuilder.AddColumn<int>(
                name: "FrontisIndex",
                table: "Pisos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Pisos_Divisiones_DivisionId",
                table: "Pisos",
                column: "DivisionId",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
