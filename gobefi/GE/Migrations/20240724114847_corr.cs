using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class corr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acciones_Medidas_MedidaId",
                table: "Acciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Acciones_Medidas_MedidaId1",
                table: "Acciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Acciones_Objetivos_ObjetivoId",
                table: "Acciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Acciones_Objetivos_ObjetivoId1",
                table: "Acciones");

            migrationBuilder.DropIndex(
                name: "IX_Acciones_MedidaId1",
                table: "Acciones");

            migrationBuilder.DropIndex(
                name: "IX_Acciones_ObjetivoId1",
                table: "Acciones");

            migrationBuilder.DropColumn(
                name: "MedidaId1",
                table: "Acciones");

            migrationBuilder.DropColumn(
                name: "ObjetivoId1",
                table: "Acciones");

            migrationBuilder.AddForeignKey(
                name: "FK_Acciones_Medidas_MedidaId",
                table: "Acciones",
                column: "MedidaId",
                principalTable: "Medidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Acciones_Objetivos_ObjetivoId",
                table: "Acciones",
                column: "ObjetivoId",
                principalTable: "Objetivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acciones_Medidas_MedidaId",
                table: "Acciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Acciones_Objetivos_ObjetivoId",
                table: "Acciones");

            migrationBuilder.AddColumn<long>(
                name: "MedidaId1",
                table: "Acciones",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ObjetivoId1",
                table: "Acciones",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Acciones_MedidaId1",
                table: "Acciones",
                column: "MedidaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Acciones_ObjetivoId1",
                table: "Acciones",
                column: "ObjetivoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Acciones_Medidas_MedidaId",
                table: "Acciones",
                column: "MedidaId",
                principalTable: "Medidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Acciones_Medidas_MedidaId1",
                table: "Acciones",
                column: "MedidaId1",
                principalTable: "Medidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Acciones_Objetivos_ObjetivoId",
                table: "Acciones",
                column: "ObjetivoId",
                principalTable: "Objetivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Acciones_Objetivos_ObjetivoId1",
                table: "Acciones",
                column: "ObjetivoId1",
                principalTable: "Objetivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
