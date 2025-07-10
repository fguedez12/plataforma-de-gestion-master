using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class AddRelationCompraEnergetico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EnergeticoId",
                table: "Compras",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCompra",
                table: "Compras",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Compras_EnergeticoId",
                table: "Compras",
                column: "EnergeticoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Energeticos_EnergeticoId",
                table: "Compras",
                column: "EnergeticoId",
                principalTable: "Energeticos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Energeticos_EnergeticoId",
                table: "Compras");

            migrationBuilder.DropIndex(
                name: "IX_Compras_EnergeticoId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "EnergeticoId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "FechaCompra",
                table: "Compras");
        }
    }
}
