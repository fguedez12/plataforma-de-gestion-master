using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class tipoartefactoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artefactos_TipoArtefactos_TipoArtefactoId1",
                table: "Artefactos");

            migrationBuilder.DropIndex(
                name: "IX_Artefactos_TipoArtefactoId1",
                table: "Artefactos");

            migrationBuilder.DropColumn(
                name: "TipoArtefactoId1",
                table: "Artefactos");

            migrationBuilder.AlterColumn<long>(
                name: "TipoArtefactoId",
                table: "Artefactos",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Artefactos_TipoArtefactoId",
                table: "Artefactos",
                column: "TipoArtefactoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artefactos_TipoArtefactos_TipoArtefactoId",
                table: "Artefactos",
                column: "TipoArtefactoId",
                principalTable: "TipoArtefactos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artefactos_TipoArtefactos_TipoArtefactoId",
                table: "Artefactos");

            migrationBuilder.DropIndex(
                name: "IX_Artefactos_TipoArtefactoId",
                table: "Artefactos");

            migrationBuilder.AlterColumn<int>(
                name: "TipoArtefactoId",
                table: "Artefactos",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "TipoArtefactoId1",
                table: "Artefactos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artefactos_TipoArtefactoId1",
                table: "Artefactos",
                column: "TipoArtefactoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Artefactos_TipoArtefactos_TipoArtefactoId1",
                table: "Artefactos",
                column: "TipoArtefactoId1",
                principalTable: "TipoArtefactos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
