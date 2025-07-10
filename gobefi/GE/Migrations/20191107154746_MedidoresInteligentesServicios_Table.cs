using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class MedidoresInteligentesServicios_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedidorInteligenteServicios",
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
                    ServicioId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedidorInteligenteServicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedidorInteligenteServicios_MedidoresInteligentes_MedidorInteligenteId",
                        column: x => x.MedidorInteligenteId,
                        principalTable: "MedidoresInteligentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedidorInteligenteServicios_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedidorInteligenteServicios_MedidorInteligenteId",
                table: "MedidorInteligenteServicios",
                column: "MedidorInteligenteId");

            migrationBuilder.CreateIndex(
                name: "IX_MedidorInteligenteServicios_ServicioId",
                table: "MedidorInteligenteServicios",
                column: "ServicioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedidorInteligenteServicios");
        }
    }
}
