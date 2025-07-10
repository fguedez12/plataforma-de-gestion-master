using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class delete_user_long_from_division : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Divisiones_AspNetUsers_UsuarioId1",
                table: "Divisiones");

            migrationBuilder.DropIndex(
                name: "IX_Divisiones_UsuarioId1",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Regiones");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Regiones");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Regiones");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Regiones");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Regiones");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Regiones");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Provincias");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Provincias");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Provincias");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Provincias");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Provincias");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Provincias");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Comunas");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Comunas");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Comunas");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Comunas");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Comunas");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Comunas");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "TipoAislaciones",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "TipoAgrupamientos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "TipoAislaciones");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "TipoAgrupamientos");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Regiones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Regiones",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Regiones",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Regiones",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Regiones",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "Regiones",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Provincias",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Provincias",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Provincias",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Provincias",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Provincias",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "Provincias",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UsuarioId",
                table: "Divisiones",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Comunas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Comunas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Comunas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Comunas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Comunas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "Comunas",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Divisiones_UsuarioId1",
                table: "Divisiones",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Divisiones_AspNetUsers_UsuarioId1",
                table: "Divisiones",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
