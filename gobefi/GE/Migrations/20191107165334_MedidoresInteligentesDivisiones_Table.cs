using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class MedidoresInteligentesDivisiones_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedidorInteligenteDivisiones",
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
                    DivisionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedidorInteligenteDivisiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedidorInteligenteDivisiones_Divisiones_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Divisiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedidorInteligenteDivisiones_MedidoresInteligentes_MedidorInteligenteId",
                        column: x => x.MedidorInteligenteId,
                        principalTable: "MedidoresInteligentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedidorInteligenteDivisiones_DivisionId",
                table: "MedidorInteligenteDivisiones",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_MedidorInteligenteDivisiones_MedidorInteligenteId",
                table: "MedidorInteligenteDivisiones",
                column: "MedidorInteligenteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedidorInteligenteDivisiones");
        }
    }
}
