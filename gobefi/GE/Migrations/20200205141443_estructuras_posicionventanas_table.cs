using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class estructuras_posicionventanas_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstructuraPosicionVentana",
                columns: table => new
                {
                    EstructuraId = table.Column<long>(nullable: false),
                    PosicionVentanaId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstructuraPosicionVentana", x => new { x.EstructuraId, x.PosicionVentanaId });
                    table.ForeignKey(
                        name: "FK_EstructuraPosicionVentana_Estructuras_EstructuraId",
                        column: x => x.EstructuraId,
                        principalTable: "Estructuras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstructuraPosicionVentana_PosicionVentanas_PosicionVentanaId",
                        column: x => x.PosicionVentanaId,
                        principalTable: "PosicionVentanas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstructuraPosicionVentana_PosicionVentanaId",
                table: "EstructuraPosicionVentana",
                column: "PosicionVentanaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstructuraPosicionVentana");
        }
    }
}
