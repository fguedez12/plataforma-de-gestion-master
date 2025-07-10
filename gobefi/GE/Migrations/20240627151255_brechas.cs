using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class brechas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brechas",
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
                    ServicioId = table.Column<long>(nullable: false),
                    Descripcion = table.Column<string>(nullable: false),
                    DimensionBrechaId = table.Column<long>(nullable: false),
                    TipoBrecha = table.Column<int>(nullable: false),
                    Titulo = table.Column<string>(nullable: false),
                    Priorizacion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brechas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brechas_DimensionBrechas_DimensionBrechaId",
                        column: x => x.DimensionBrechaId,
                        principalTable: "DimensionBrechas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Brechas_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrechaUnidades",
                columns: table => new
                {
                    BrechaId = table.Column<long>(nullable: false),
                    DivisionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrechaUnidades", x => new { x.BrechaId, x.DivisionId });
                    table.ForeignKey(
                        name: "FK_BrechaUnidades_Brechas_BrechaId",
                        column: x => x.BrechaId,
                        principalTable: "Brechas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BrechaUnidades_Divisiones_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Divisiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brechas_DimensionBrechaId",
                table: "Brechas",
                column: "DimensionBrechaId");

            migrationBuilder.CreateIndex(
                name: "IX_Brechas_ServicioId",
                table: "Brechas",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_BrechaUnidades_DivisionId",
                table: "BrechaUnidades",
                column: "DivisionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrechaUnidades");

            migrationBuilder.DropTable(
                name: "Brechas");
        }
    }
}
