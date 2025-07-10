using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class estructuraTipoSombreado_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstructuraTipodeSombreado",
                columns: table => new
                {
                    EstructuraId = table.Column<long>(nullable: false),
                    TipoSombreadoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstructuraTipodeSombreado", x => new { x.EstructuraId, x.TipoSombreadoId });
                    table.ForeignKey(
                        name: "FK_EstructuraTipodeSombreado_Estructuras_EstructuraId",
                        column: x => x.EstructuraId,
                        principalTable: "Estructuras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstructuraTipodeSombreado_TipoSombreados_TipoSombreadoId",
                        column: x => x.TipoSombreadoId,
                        principalTable: "TipoSombreados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstructuraTipodeSombreado_TipoSombreadoId",
                table: "EstructuraTipodeSombreado",
                column: "TipoSombreadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstructuraTipodeSombreado");
        }
    }
}
