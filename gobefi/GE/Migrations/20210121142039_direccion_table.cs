using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class direccion_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DireccionInmuebleId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Direccion",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Calle = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    DireccionCompleta = table.Column<string>(nullable: true),
                    RegionId = table.Column<long>(nullable: false),
                    ProviciaId = table.Column<long>(nullable: false),
                    ComunaId = table.Column<long>(nullable: false),
                    ProvinciaId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direccion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Direccion_Comunas_ComunaId",
                        column: x => x.ComunaId,
                        principalTable: "Comunas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Direccion_Provincias_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "Provincias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Direccion_Regiones_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Divisiones_DireccionInmuebleId",
                table: "Divisiones",
                column: "DireccionInmuebleId");

            migrationBuilder.CreateIndex(
                name: "IX_Direccion_ComunaId",
                table: "Direccion",
                column: "ComunaId");

            migrationBuilder.CreateIndex(
                name: "IX_Direccion_ProvinciaId",
                table: "Direccion",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Direccion_RegionId",
                table: "Direccion",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Divisiones_Direccion_DireccionInmuebleId",
                table: "Divisiones",
                column: "DireccionInmuebleId",
                principalTable: "Direccion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Divisiones_Direccion_DireccionInmuebleId",
                table: "Divisiones");

            migrationBuilder.DropTable(
                name: "Direccion");

            migrationBuilder.DropIndex(
                name: "IX_Divisiones_DireccionInmuebleId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "DireccionInmuebleId",
                table: "Divisiones");
        }
    }
}
