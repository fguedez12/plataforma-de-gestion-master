using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class MedidoresInteligentesEdificios_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedidorInteligenteEdificios",
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
                    MedidorInteligenteId = table.Column<long>(nullable: false),
                    EdificioId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedidorInteligenteEdificios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedidorInteligenteEdificios_Edificios_EdificioId",
                        column: x => x.EdificioId,
                        principalTable: "Edificios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedidorInteligenteEdificios_MedidoresInteligentes_MedidorInteligenteId",
                        column: x => x.MedidorInteligenteId,
                        principalTable: "MedidoresInteligentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedidorInteligenteEdificios_EdificioId",
                table: "MedidorInteligenteEdificios",
                column: "EdificioId");

            migrationBuilder.CreateIndex(
                name: "IX_MedidorInteligenteEdificios_MedidorInteligenteId",
                table: "MedidorInteligenteEdificios",
                column: "MedidorInteligenteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedidorInteligenteEdificios");
        }
    }
}
