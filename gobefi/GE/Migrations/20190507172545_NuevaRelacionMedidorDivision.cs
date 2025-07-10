using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class NuevaRelacionMedidorDivision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldId",
                table: "NumeroClientes");

            migrationBuilder.DropColumn(
                name: "OldId",
                table: "Medidores");

            migrationBuilder.DropColumn(
                name: "OldId",
                table: "Compras");

            migrationBuilder.RenameColumn(
                name: "EstadoValidacion",
                table: "Compras",
                newName: "EstadoValidacionId");

            migrationBuilder.CreateTable(
                name: "EstadoValidacion",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoValidacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedidorDivision",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Version = table.Column<long>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    DivisionId = table.Column<long>(nullable: false),
                    MedidorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedidorDivision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedidorDivision_Medidores_MedidorId",
                        column: x => x.MedidorId,
                        principalTable: "Medidores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedidorDivision_MedidorId",
                table: "MedidorDivision",
                column: "MedidorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstadoValidacion");

            migrationBuilder.DropTable(
                name: "MedidorDivision");

            migrationBuilder.RenameColumn(
                name: "EstadoValidacionId",
                table: "Compras",
                newName: "EstadoValidacion");

            migrationBuilder.AddColumn<long>(
                name: "OldId",
                table: "NumeroClientes",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OldId",
                table: "Medidores",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OldId",
                table: "Compras",
                nullable: true);
        }
    }
}
