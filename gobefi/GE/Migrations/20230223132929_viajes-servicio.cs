using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class viajesservicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viajes_Divisiones_DivisionId",
                table: "Viajes");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "Viajes",
                newName: "ServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_Viajes_DivisionId",
                table: "Viajes",
                newName: "IX_Viajes_ServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Viajes_Servicios_ServicioId",
                table: "Viajes",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viajes_Servicios_ServicioId",
                table: "Viajes");

            migrationBuilder.RenameColumn(
                name: "ServicioId",
                table: "Viajes",
                newName: "DivisionId");

            migrationBuilder.RenameIndex(
                name: "IX_Viajes_ServicioId",
                table: "Viajes",
                newName: "IX_Viajes_DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Viajes_Divisiones_DivisionId",
                table: "Viajes",
                column: "DivisionId",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
