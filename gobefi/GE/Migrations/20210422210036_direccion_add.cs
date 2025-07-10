using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class direccion_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Direcciones",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Calle = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    DireccionCompleta = table.Column<string>(nullable: true),
                    RegionId = table.Column<long>(nullable: false),
                    ProvinciaId = table.Column<long>(nullable: false),
                    ComunaId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direcciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Direcciones_Comunas_ComunaId",
                        column: x => x.ComunaId,
                        principalTable: "Comunas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Direcciones_Provincias_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "Provincias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Direcciones_Regiones_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Divisiones_DireccionInmuebleId",
                table: "Divisiones",
                column: "DireccionInmuebleId");

            migrationBuilder.CreateIndex(
                name: "IX_Direcciones_ComunaId",
                table: "Direcciones",
                column: "ComunaId");

            migrationBuilder.CreateIndex(
                name: "IX_Direcciones_ProvinciaId",
                table: "Direcciones",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Direcciones_RegionId",
                table: "Direcciones",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Divisiones_Direcciones_DireccionInmuebleId",
                table: "Divisiones",
                column: "DireccionInmuebleId",
                principalTable: "Direcciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Divisiones_Direcciones_DireccionInmuebleId",
                table: "Divisiones");

            migrationBuilder.DropTable(
                name: "Direcciones");

            migrationBuilder.DropIndex(
                name: "IX_Divisiones_DireccionInmuebleId",
                table: "Divisiones");
        }
    }
}
