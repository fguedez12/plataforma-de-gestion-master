using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class tablasp3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aislaciones",
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
                    table.PrimaryKey("PK_Aislaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estructuras",
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
                    table.PrimaryKey("PK_Estructuras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materialidades",
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
                    table.PrimaryKey("PK_Materialidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstructuraAislaciones",
                columns: table => new
                {
                    EstructuraId = table.Column<long>(nullable: false),
                    AislacionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstructuraAislaciones", x => new { x.EstructuraId, x.AislacionId });
                    table.ForeignKey(
                        name: "FK_EstructuraAislaciones_Aislaciones_AislacionId",
                        column: x => x.AislacionId,
                        principalTable: "Aislaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstructuraAislaciones_Estructuras_EstructuraId",
                        column: x => x.EstructuraId,
                        principalTable: "Estructuras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstructuraMaterialidades",
                columns: table => new
                {
                    EstructuraId = table.Column<long>(nullable: false),
                    MaterialidadId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstructuraMaterialidades", x => new { x.EstructuraId, x.MaterialidadId });
                    table.ForeignKey(
                        name: "FK_EstructuraMaterialidades_Estructuras_EstructuraId",
                        column: x => x.EstructuraId,
                        principalTable: "Estructuras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstructuraMaterialidades_Materialidades_MaterialidadId",
                        column: x => x.MaterialidadId,
                        principalTable: "Materialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstructuraAislaciones_AislacionId",
                table: "EstructuraAislaciones",
                column: "AislacionId");

            migrationBuilder.CreateIndex(
                name: "IX_EstructuraMaterialidades_MaterialidadId",
                table: "EstructuraMaterialidades",
                column: "MaterialidadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstructuraAislaciones");

            migrationBuilder.DropTable(
                name: "EstructuraMaterialidades");

            migrationBuilder.DropTable(
                name: "Aislaciones");

            migrationBuilder.DropTable(
                name: "Estructuras");

            migrationBuilder.DropTable(
                name: "Materialidades");
        }
    }
}
