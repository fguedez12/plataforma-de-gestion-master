using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class difusionpoliticaid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PoliticaId",
                table: "Documentos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_PoliticaId",
                table: "Documentos",
                column: "PoliticaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_Documentos_PoliticaId",
                table: "Documentos",
                column: "PoliticaId",
                principalTable: "Documentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_Documentos_PoliticaId",
                table: "Documentos");

            migrationBuilder.DropIndex(
                name: "IX_Documentos_PoliticaId",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "PoliticaId",
                table: "Documentos");
        }
    }
}
