using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class tipocalefaccionenergetico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoEquipoCalefaccionEnergetico",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoEquipoCalefaccionId = table.Column<long>(nullable: false),
                    EnergeticoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEquipoCalefaccionEnergetico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TipoEquipoCalefaccionEnergetico_Energeticos_EnergeticoId",
                        column: x => x.EnergeticoId,
                        principalTable: "Energeticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TipoEquipoCalefaccionEnergetico_TiposEquiposCalefaccion_TipoEquipoCalefaccionId",
                        column: x => x.TipoEquipoCalefaccionId,
                        principalTable: "TiposEquiposCalefaccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TipoEquipoCalefaccionEnergetico_EnergeticoId",
                table: "TipoEquipoCalefaccionEnergetico",
                column: "EnergeticoId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoEquipoCalefaccionEnergetico_TipoEquipoCalefaccionId",
                table: "TipoEquipoCalefaccionEnergetico",
                column: "TipoEquipoCalefaccionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoEquipoCalefaccionEnergetico");
        }
    }
}
