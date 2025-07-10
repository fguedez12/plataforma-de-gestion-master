using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class empresaDistribuidoraComunas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmpresasDistribuidoraComunas",
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
                    EmpresaId = table.Column<long>(nullable: false),
                    ComunaId = table.Column<long>(nullable: false),
                    EmpresaDistribuidoraId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresasDistribuidoraComunas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpresasDistribuidoraComunas_Comunas_ComunaId",
                        column: x => x.ComunaId,
                        principalTable: "Comunas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpresasDistribuidoraComunas_EmpresaDistribuidoras_EmpresaDistribuidoraId",
                        column: x => x.EmpresaDistribuidoraId,
                        principalTable: "EmpresaDistribuidoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmpresasDistribuidoraComunas_ComunaId",
                table: "EmpresasDistribuidoraComunas",
                column: "ComunaId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresasDistribuidoraComunas_EmpresaDistribuidoraId",
                table: "EmpresasDistribuidoraComunas",
                column: "EmpresaDistribuidoraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpresasDistribuidoraComunas");
        }
    }
}
