using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class muros_campos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AislacionExtId",
                table: "Muros",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AislacionIntId",
                table: "Muros",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MaterialidadId",
                table: "Muros",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Superficie",
                table: "Muros",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Muros_AislacionExtId",
                table: "Muros",
                column: "AislacionExtId");

            migrationBuilder.CreateIndex(
                name: "IX_Muros_AislacionIntId",
                table: "Muros",
                column: "AislacionIntId");

            migrationBuilder.CreateIndex(
                name: "IX_Muros_MaterialidadId",
                table: "Muros",
                column: "MaterialidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Muros_Aislaciones_AislacionExtId",
                table: "Muros",
                column: "AislacionExtId",
                principalTable: "Aislaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Muros_Aislaciones_AislacionIntId",
                table: "Muros",
                column: "AislacionIntId",
                principalTable: "Aislaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Muros_Materialidades_MaterialidadId",
                table: "Muros",
                column: "MaterialidadId",
                principalTable: "Materialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Muros_Aislaciones_AislacionExtId",
                table: "Muros");

            migrationBuilder.DropForeignKey(
                name: "FK_Muros_Aislaciones_AislacionIntId",
                table: "Muros");

            migrationBuilder.DropForeignKey(
                name: "FK_Muros_Materialidades_MaterialidadId",
                table: "Muros");

            migrationBuilder.DropIndex(
                name: "IX_Muros_AislacionExtId",
                table: "Muros");

            migrationBuilder.DropIndex(
                name: "IX_Muros_AislacionIntId",
                table: "Muros");

            migrationBuilder.DropIndex(
                name: "IX_Muros_MaterialidadId",
                table: "Muros");

            migrationBuilder.DropColumn(
                name: "AislacionExtId",
                table: "Muros");

            migrationBuilder.DropColumn(
                name: "AislacionIntId",
                table: "Muros");

            migrationBuilder.DropColumn(
                name: "MaterialidadId",
                table: "Muros");

            migrationBuilder.DropColumn(
                name: "Superficie",
                table: "Muros");
        }
    }
}
