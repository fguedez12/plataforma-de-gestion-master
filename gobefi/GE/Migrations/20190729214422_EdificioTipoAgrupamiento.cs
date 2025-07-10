using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class EdificioTipoAgrupamiento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_Divisiones_TipoAgrupamientos_TipoAgrupamientoId",
               table: "Divisiones");

            //no existe
            //migrationBuilder.DropIndex(
            //    name: "IX_Divisiones_TipoAgrupamientoId",
            //    table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "TipoAgrupamientoId",
                table: "Divisiones");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "TipoAgrupamientos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TipoAgrupamientos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TipoAgrupamientos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TipoAgrupamientos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TipoAgrupamientos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "TipoAgrupamientos",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TipoAgrupamientoId",
                table: "Edificios",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Edificios_TipoAgrupamientoId",
                table: "Edificios",
                column: "TipoAgrupamientoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Edificios_TipoAgrupamientos_TipoAgrupamientoId",
                table: "Edificios",
                column: "TipoAgrupamientoId",
                principalTable: "TipoAgrupamientos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Edificios_TipoAgrupamientos_TipoAgrupamientoId",
                table: "Edificios");

            migrationBuilder.DropIndex(
                name: "IX_Edificios_TipoAgrupamientoId",
                table: "Edificios");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "TipoAgrupamientos");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TipoAgrupamientos");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TipoAgrupamientos");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TipoAgrupamientos");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TipoAgrupamientos");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "TipoAgrupamientos");

            migrationBuilder.DropColumn(
                name: "TipoAgrupamientoId",
                table: "Edificios");

            migrationBuilder.AddColumn<long>(
                name: "TipoAgrupamientoId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Divisiones_TipoAgrupamientoId",
                table: "Divisiones",
                column: "TipoAgrupamientoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Divisiones_TipoAgrupamientos_TipoAgrupamientoId",
                table: "Divisiones",
                column: "TipoAgrupamientoId",
                principalTable: "TipoAgrupamientos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
