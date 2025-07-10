using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class posicion_ventanas_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoVentanas");

            migrationBuilder.AddColumn<long>(
                name: "PosicionVentanaId",
                table: "Ventanas",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "PosicionVentanas",
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
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosicionVentanas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ventanas_PosicionVentanaId",
                table: "Ventanas",
                column: "PosicionVentanaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventanas_PosicionVentanas_PosicionVentanaId",
                table: "Ventanas",
                column: "PosicionVentanaId",
                principalTable: "PosicionVentanas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventanas_PosicionVentanas_PosicionVentanaId",
                table: "Ventanas");

            migrationBuilder.DropTable(
                name: "PosicionVentanas");

            migrationBuilder.DropIndex(
                name: "IX_Ventanas_PosicionVentanaId",
                table: "Ventanas");

            migrationBuilder.DropColumn(
                name: "PosicionVentanaId",
                table: "Ventanas");

            migrationBuilder.CreateTable(
                name: "TipoVentanas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FactorVentana = table.Column<double>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoVentanas", x => x.Id);
                });
        }
    }
}
