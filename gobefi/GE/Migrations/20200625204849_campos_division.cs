using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class campos_division : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Calle",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ComunaId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Numero",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Divisiones_ComunaId",
                table: "Divisiones",
                column: "ComunaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Divisiones_Comunas_ComunaId",
                table: "Divisiones",
                column: "ComunaId",
                principalTable: "Comunas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Divisiones_Comunas_ComunaId",
                table: "Divisiones");

            migrationBuilder.DropIndex(
                name: "IX_Divisiones_ComunaId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "Calle",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "ComunaId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Divisiones");
        }
    }
}
