using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class userprofilingrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuariosDivisiones",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(nullable: false),
                    DivisionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosDivisiones", x => new { x.DivisionId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_UsuariosDivisiones_Divisiones_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Divisiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosDivisiones_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosInstituciones",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(nullable: false),
                    InstitucionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosInstituciones", x => new { x.InstitucionId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_UsuariosInstituciones_Instituciones_InstitucionId",
                        column: x => x.InstitucionId,
                        principalTable: "Instituciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosInstituciones_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosServicios",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(nullable: false),
                    ServicioId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosServicios", x => new { x.ServicioId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_UsuariosServicios_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosServicios_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosDivisiones_UsuarioId",
                table: "UsuariosDivisiones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosInstituciones_UsuarioId",
                table: "UsuariosInstituciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosServicios_UsuarioId",
                table: "UsuariosServicios",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuariosDivisiones");

            migrationBuilder.DropTable(
                name: "UsuariosInstituciones");

            migrationBuilder.DropTable(
                name: "UsuariosServicios");
        }
    }
}
