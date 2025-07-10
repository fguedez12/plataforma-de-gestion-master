using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class Indicadores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ObjetivoId",
                table: "Objetivos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Indicadores",
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
                    DimensionBrechaId = table.Column<long>(nullable: false),
                    ObjetivoId = table.Column<long>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Descripcion = table.Column<string>(nullable: false),
                    Numerador = table.Column<string>(nullable: true),
                    Denominador = table.Column<string>(nullable: true),
                    UnidadMedida = table.Column<string>(nullable: true),
                    Valor = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Indicadores_DimensionBrechas_DimensionBrechaId",
                        column: x => x.DimensionBrechaId,
                        principalTable: "DimensionBrechas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Indicadores_Objetivos_ObjetivoId",
                        column: x => x.ObjetivoId,
                        principalTable: "Objetivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Objetivos_ObjetivoId",
                table: "Objetivos",
                column: "ObjetivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Indicadores_DimensionBrechaId",
                table: "Indicadores",
                column: "DimensionBrechaId");

            migrationBuilder.CreateIndex(
                name: "IX_Indicadores_ObjetivoId",
                table: "Indicadores",
                column: "ObjetivoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Objetivos_Objetivos_ObjetivoId",
                table: "Objetivos",
                column: "ObjetivoId",
                principalTable: "Objetivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objetivos_Objetivos_ObjetivoId",
                table: "Objetivos");

            migrationBuilder.DropTable(
                name: "Indicadores");

            migrationBuilder.DropIndex(
                name: "IX_Objetivos_ObjetivoId",
                table: "Objetivos");

            migrationBuilder.DropColumn(
                name: "ObjetivoId",
                table: "Objetivos");
        }
    }
}
