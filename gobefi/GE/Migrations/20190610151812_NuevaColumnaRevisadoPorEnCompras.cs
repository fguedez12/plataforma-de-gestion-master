using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class NuevaColumnaRevisadoPorEnCompras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EstadoValidacionId",
                table: "Compras",
                nullable: true,
                defaultValue: "sin_revision",
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "sin_revision");

            migrationBuilder.AddColumn<string>(
                name: "RevisadoPor",
                table: "Compras",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compras_EstadoValidacionId",
                table: "Compras",
                column: "EstadoValidacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_EstadoValidacion_EstadoValidacionId",
                table: "Compras",
                column: "EstadoValidacionId",
                principalTable: "EstadoValidacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_EstadoValidacion_EstadoValidacionId",
                table: "Compras");

            migrationBuilder.DropIndex(
                name: "IX_Compras_EstadoValidacionId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "RevisadoPor",
                table: "Compras");

            migrationBuilder.AlterColumn<string>(
                name: "EstadoValidacionId",
                table: "Compras",
                nullable: true,
                defaultValue: "sin_revision",
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "sin_revision");
        }
    }
}
