using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class suelosTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suelos",
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
                    MaterialidadId = table.Column<long>(nullable: false),
                    AislacionId = table.Column<long>(nullable: false),
                    PisoId = table.Column<long>(nullable: false),
                    Superficie = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suelos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suelos_Aislaciones_AislacionId",
                        column: x => x.AislacionId,
                        principalTable: "Aislaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Suelos_Materialidades_MaterialidadId",
                        column: x => x.MaterialidadId,
                        principalTable: "Materialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Suelos_Pisos_PisoId",
                        column: x => x.PisoId,
                        principalTable: "Pisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suelos_AislacionId",
                table: "Suelos",
                column: "AislacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Suelos_MaterialidadId",
                table: "Suelos",
                column: "MaterialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Suelos_PisoId",
                table: "Suelos",
                column: "PisoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Suelos");
        }
    }
}
