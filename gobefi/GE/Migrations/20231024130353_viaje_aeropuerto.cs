using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class viaje_aeropuerto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AeropuertoDestinoId",
                table: "Viajes",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AeropuertoOrigenId",
                table: "Viajes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Viajes_AeropuertoDestinoId",
                table: "Viajes",
                column: "AeropuertoDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Viajes_AeropuertoOrigenId",
                table: "Viajes",
                column: "AeropuertoOrigenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Viajes_Aeropuertos_AeropuertoDestinoId",
                table: "Viajes",
                column: "AeropuertoDestinoId",
                principalTable: "Aeropuertos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Viajes_Aeropuertos_AeropuertoOrigenId",
                table: "Viajes",
                column: "AeropuertoOrigenId",
                principalTable: "Aeropuertos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viajes_Aeropuertos_AeropuertoDestinoId",
                table: "Viajes");

            migrationBuilder.DropForeignKey(
                name: "FK_Viajes_Aeropuertos_AeropuertoOrigenId",
                table: "Viajes");

            migrationBuilder.DropIndex(
                name: "IX_Viajes_AeropuertoDestinoId",
                table: "Viajes");

            migrationBuilder.DropIndex(
                name: "IX_Viajes_AeropuertoOrigenId",
                table: "Viajes");

            migrationBuilder.DropColumn(
                name: "AeropuertoDestinoId",
                table: "Viajes");

            migrationBuilder.DropColumn(
                name: "AeropuertoOrigenId",
                table: "Viajes");
        }
    }
}
