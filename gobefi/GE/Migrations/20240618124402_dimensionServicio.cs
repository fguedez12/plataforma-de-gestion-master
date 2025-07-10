using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class dimensionServicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DimensionServicios",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DimensionBrechaId = table.Column<long>(nullable: false),
                    ServicioId = table.Column<long>(nullable: false),
                    IngresoObservacion = table.Column<bool>(nullable: false),
                    Observacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimensionServicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DimensionServicios_DimensionBrechas_DimensionBrechaId",
                        column: x => x.DimensionBrechaId,
                        principalTable: "DimensionBrechas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DimensionServicios_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DimensionServicios_DimensionBrechaId",
                table: "DimensionServicios",
                column: "DimensionBrechaId");

            migrationBuilder.CreateIndex(
                name: "IX_DimensionServicios_ServicioId",
                table: "DimensionServicios",
                column: "ServicioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DimensionServicios");
        }
    }
}
