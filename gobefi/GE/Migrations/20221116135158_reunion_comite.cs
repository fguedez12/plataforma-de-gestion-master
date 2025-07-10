using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class reunion_comite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ActaComiteId",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Documentos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_ActaComiteId",
                table: "Documentos",
                column: "ActaComiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_Documentos_ActaComiteId",
                table: "Documentos",
                column: "ActaComiteId",
                principalTable: "Documentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_Documentos_ActaComiteId",
                table: "Documentos");

            migrationBuilder.DropIndex(
                name: "IX_Documentos_ActaComiteId",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "ActaComiteId",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Documentos");
        }
    }
}
