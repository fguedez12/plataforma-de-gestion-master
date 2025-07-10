using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class perfilamiento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuarioDivisiones",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(nullable: false),
                    DivisionId = table.Column<int>(nullable: false),
                    DivisionId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioDivisiones", x => new { x.DivisionId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_UsuarioDivisiones_Divisiones_DivisionId1",
                        column: x => x.DivisionId1,
                        principalTable: "Divisiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioDivisiones_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioInstituciones",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(nullable: false),
                    InstitucionId = table.Column<int>(nullable: false),
                    InstitucionId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioInstituciones", x => new { x.InstitucionId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_UsuarioInstituciones_Instituciones_InstitucionId1",
                        column: x => x.InstitucionId1,
                        principalTable: "Instituciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioInstituciones_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioServicios",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(nullable: false),
                    ServicioId = table.Column<int>(nullable: false),
                    ServicioId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioServicios", x => new { x.ServicioId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_UsuarioServicios_Servicios_ServicioId1",
                        column: x => x.ServicioId1,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioServicios_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioDivisiones_DivisionId1",
                table: "UsuarioDivisiones",
                column: "DivisionId1");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioDivisiones_UsuarioId",
                table: "UsuarioDivisiones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioInstituciones_InstitucionId1",
                table: "UsuarioInstituciones",
                column: "InstitucionId1");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioInstituciones_UsuarioId",
                table: "UsuarioInstituciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioServicios_ServicioId1",
                table: "UsuarioServicios",
                column: "ServicioId1");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioServicios_UsuarioId",
                table: "UsuarioServicios",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioDivisiones");

            migrationBuilder.DropTable(
                name: "UsuarioInstituciones");

            migrationBuilder.DropTable(
                name: "UsuarioServicios");
        }
    }
}
