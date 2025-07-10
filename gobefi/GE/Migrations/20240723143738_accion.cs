using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class accion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MedidaId",
                table: "Medidas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Acciones",
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
                    DimensionBrechaId = table.Column<long>(nullable: false),
                    ObjetivoId = table.Column<long>(nullable: false),
                    MedidaId = table.Column<long>(nullable: false),
                    OtraMedida = table.Column<string>(nullable: true),
                    MedidaDescripcion = table.Column<string>(nullable: false),
                    Cobertura = table.Column<string>(nullable: false),
                    NivelMedida = table.Column<string>(nullable: false),
                    GestorRespnsableId = table.Column<string>(nullable: false),
                    AdjuntoUrl = table.Column<string>(nullable: true),
                    AdjuntoNombre = table.Column<string>(nullable: true),
                    OtroServicio = table.Column<string>(nullable: false),
                    PresupuestoIngenieria = table.Column<int>(nullable: true),
                    PresupuestoIngenieriaPedido = table.Column<bool>(nullable: true),
                    PresupuestoImplementacion = table.Column<int>(nullable: true),
                    PresupuestoImplementacionPedido = table.Column<bool>(nullable: true),
                    ObjetivoId1 = table.Column<long>(nullable: true),
                    MedidaId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acciones_DimensionBrechas_DimensionBrechaId",
                        column: x => x.DimensionBrechaId,
                        principalTable: "DimensionBrechas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Acciones_Medidas_MedidaId",
                        column: x => x.MedidaId,
                        principalTable: "Medidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acciones_Medidas_MedidaId1",
                        column: x => x.MedidaId1,
                        principalTable: "Medidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acciones_Objetivos_ObjetivoId",
                        column: x => x.ObjetivoId,
                        principalTable: "Objetivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acciones_Objetivos_ObjetivoId1",
                        column: x => x.ObjetivoId1,
                        principalTable: "Objetivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccionServicios",
                columns: table => new
                {
                    AccionId = table.Column<long>(nullable: false),
                    ServicioId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccionServicios", x => new { x.AccionId, x.ServicioId });
                    table.ForeignKey(
                        name: "FK_AccionServicios_Acciones_AccionId",
                        column: x => x.AccionId,
                        principalTable: "Acciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccionServicios_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccionUnidades",
                columns: table => new
                {
                    AccionId = table.Column<long>(nullable: false),
                    DivisionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccionUnidades", x => new { x.AccionId, x.DivisionId });
                    table.ForeignKey(
                        name: "FK_AccionUnidades_Acciones_AccionId",
                        column: x => x.AccionId,
                        principalTable: "Acciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccionUnidades_Divisiones_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Divisiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medidas_MedidaId",
                table: "Medidas",
                column: "MedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_Acciones_DimensionBrechaId",
                table: "Acciones",
                column: "DimensionBrechaId");

            migrationBuilder.CreateIndex(
                name: "IX_Acciones_MedidaId",
                table: "Acciones",
                column: "MedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_Acciones_MedidaId1",
                table: "Acciones",
                column: "MedidaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Acciones_ObjetivoId",
                table: "Acciones",
                column: "ObjetivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Acciones_ObjetivoId1",
                table: "Acciones",
                column: "ObjetivoId1");

            migrationBuilder.CreateIndex(
                name: "IX_AccionServicios_ServicioId",
                table: "AccionServicios",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_AccionUnidades_DivisionId",
                table: "AccionUnidades",
                column: "DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medidas_Medidas_MedidaId",
                table: "Medidas",
                column: "MedidaId",
                principalTable: "Medidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medidas_Medidas_MedidaId",
                table: "Medidas");

            migrationBuilder.DropTable(
                name: "AccionServicios");

            migrationBuilder.DropTable(
                name: "AccionUnidades");

            migrationBuilder.DropTable(
                name: "Acciones");

            migrationBuilder.DropIndex(
                name: "IX_Medidas_MedidaId",
                table: "Medidas");

            migrationBuilder.DropColumn(
                name: "MedidaId",
                table: "Medidas");
        }
    }
}
