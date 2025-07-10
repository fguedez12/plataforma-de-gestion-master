using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class administracionServicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Divisiones_AdministracionServicioId",
                table: "Divisiones",
                column: "AdministracionServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Divisiones_Servicios_AdministracionServicioId",
                table: "Divisiones",
                column: "AdministracionServicioId",
                principalTable: "Servicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Divisiones_Servicios_AdministracionServicioId",
                table: "Divisiones");

            migrationBuilder.DropIndex(
                name: "IX_Divisiones_AdministracionServicioId",
                table: "Divisiones");
        }
    }
}
