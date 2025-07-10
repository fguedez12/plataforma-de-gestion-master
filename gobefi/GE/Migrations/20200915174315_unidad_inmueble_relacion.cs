using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class unidad_inmueble_relacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnidadesInmuebles",
                columns: table => new
                {
                    InmuebleId = table.Column<long>(nullable: false),
                    UnidadId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesInmuebles", x => new { x.UnidadId, x.InmuebleId });
                    table.ForeignKey(
                        name: "FK_UnidadesInmuebles_Divisiones_InmuebleId",
                        column: x => x.InmuebleId,
                        principalTable: "Divisiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnidadesInmuebles_Unidades_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnidadesInmuebles_InmuebleId",
                table: "UnidadesInmuebles",
                column: "InmuebleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnidadesInmuebles");
        }
    }
}
