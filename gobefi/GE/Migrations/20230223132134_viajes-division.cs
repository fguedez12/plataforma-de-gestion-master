using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class viajesdivision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viaje_Servicios_ServicioId",
                table: "Viaje");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Viaje",
                table: "Viaje");

            migrationBuilder.RenameTable(
                name: "Viaje",
                newName: "Viajes");

            migrationBuilder.RenameColumn(
                name: "ServicioId",
                table: "Viajes",
                newName: "DivisionId");

            migrationBuilder.RenameIndex(
                name: "IX_Viaje_ServicioId",
                table: "Viajes",
                newName: "IX_Viajes_DivisionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Viajes",
                table: "Viajes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Viajes_Divisiones_DivisionId",
                table: "Viajes",
                column: "DivisionId",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viajes_Divisiones_DivisionId",
                table: "Viajes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Viajes",
                table: "Viajes");

            migrationBuilder.RenameTable(
                name: "Viajes",
                newName: "Viaje");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "Viaje",
                newName: "ServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_Viajes_DivisionId",
                table: "Viaje",
                newName: "IX_Viaje_ServicioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Viaje",
                table: "Viaje",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Viaje_Servicios_ServicioId",
                table: "Viaje",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
