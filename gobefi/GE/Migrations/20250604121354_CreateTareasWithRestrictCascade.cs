using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class CreateTareasWithRestrictCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanGestion_Tareas",
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
                    AccionId = table.Column<long>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 200, nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: false),
                    Responsable = table.Column<string>(maxLength: 100, nullable: false),
                    EstadoAvance = table.Column<string>(maxLength: 50, nullable: false),
                    Observaciones = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanGestion_Tareas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanGestion_Tareas_Acciones_AccionId",
                        column: x => x.AccionId,
                        principalTable: "Acciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanGestion_Tareas_DimensionBrechas_DimensionBrechaId",
                        column: x => x.DimensionBrechaId,
                        principalTable: "DimensionBrechas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanGestion_Tareas_AccionId",
                table: "PlanGestion_Tareas",
                column: "AccionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanGestion_Tareas_DimensionBrechaId",
                table: "PlanGestion_Tareas",
                column: "DimensionBrechaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanGestion_Tareas");
        }
    }
}
