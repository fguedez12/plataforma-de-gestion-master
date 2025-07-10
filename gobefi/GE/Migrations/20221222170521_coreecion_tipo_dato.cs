using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class coreecion_tipo_dato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resmas_Divisiones_DivisionId1",
                table: "Resmas");

            migrationBuilder.DropIndex(
                name: "IX_Resmas_DivisionId1",
                table: "Resmas");

            migrationBuilder.DropColumn(
                name: "DivisionId1",
                table: "Resmas");

            migrationBuilder.AlterColumn<long>(
                name: "DivisionId",
                table: "Resmas",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Resmas_DivisionId",
                table: "Resmas",
                column: "DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resmas_Divisiones_DivisionId",
                table: "Resmas",
                column: "DivisionId",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resmas_Divisiones_DivisionId",
                table: "Resmas");

            migrationBuilder.DropIndex(
                name: "IX_Resmas_DivisionId",
                table: "Resmas");

            migrationBuilder.AlterColumn<int>(
                name: "DivisionId",
                table: "Resmas",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "DivisionId1",
                table: "Resmas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resmas_DivisionId1",
                table: "Resmas",
                column: "DivisionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Resmas_Divisiones_DivisionId1",
                table: "Resmas",
                column: "DivisionId1",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
