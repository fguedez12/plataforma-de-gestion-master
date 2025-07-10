using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class removed_IAuditable_from_tipos_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "TipoArchivos");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TipoArchivos");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TipoArchivos");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TipoArchivos");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TipoArchivos");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "TipoArchivos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "TipoArchivos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TipoArchivos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TipoArchivos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TipoArchivos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TipoArchivos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "TipoArchivos",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
