using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class doumento_servicio_relacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "servicioId",
                table: "Documentos",
                newName: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_ServicioId",
                table: "Documentos",
                column: "ServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_Servicios_ServicioId",
                table: "Documentos",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_Servicios_ServicioId",
                table: "Documentos");

            migrationBuilder.DropIndex(
                name: "IX_Documentos_ServicioId",
                table: "Documentos");

            migrationBuilder.RenameColumn(
                name: "ServicioId",
                table: "Documentos",
                newName: "servicioId");
        }
    }
}
