using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class unidadesnotauditable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoUsoModel");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Unidades");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Unidades");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Unidades");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Unidades");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Unidades");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Unidades");

            //migrationBuilder.AddColumn<long>(
            //    name: "MedidoresId",
            //    table: "NumeroClientes",
            //    nullable: false,
            //    defaultValue: 0L);

            //migrationBuilder.AddColumn<long>(
            //    name: "TipoTarifaId",
            //    table: "NumeroClientes",
            //    nullable: false,
            //    defaultValue: 0L);

            //migrationBuilder.AddColumn<bool>(
            //    name: "Compartido",
            //    table: "Medidores",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.CreateIndex(
            //    name: "IX_NumeroClientes_TipoTarifaId",
            //    table: "NumeroClientes",
            //    column: "TipoTarifaId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_NumeroClientes_TipoTarifas_TipoTarifaId",
            //    table: "NumeroClientes",
            //    column: "TipoTarifaId",
            //    principalTable: "TipoTarifas",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumeroClientes_TipoTarifas_TipoTarifaId",
                table: "NumeroClientes");

            migrationBuilder.DropIndex(
                name: "IX_NumeroClientes_TipoTarifaId",
                table: "NumeroClientes");

            migrationBuilder.DropColumn(
                name: "MedidoresId",
                table: "NumeroClientes");

            migrationBuilder.DropColumn(
                name: "TipoTarifaId",
                table: "NumeroClientes");

            migrationBuilder.DropColumn(
                name: "Compartido",
                table: "Medidores");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Unidades",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Unidades",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Unidades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Unidades",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Unidades",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "Unidades",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "TipoUsoModel",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsoModel", x => x.Id);
                });
        }
    }
}
