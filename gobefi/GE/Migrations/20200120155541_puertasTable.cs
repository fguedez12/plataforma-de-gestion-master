using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class puertasTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Componente_TipoPuertas_TipoPuertaId",
                table: "Componente");

            migrationBuilder.DropIndex(
                name: "IX_Componente_TipoPuertaId",
                table: "Componente");

            migrationBuilder.DropColumn(
                name: "Ancho",
                table: "Componente");

            migrationBuilder.DropColumn(
                name: "Largo",
                table: "Componente");

            migrationBuilder.DropColumn(
                name: "TipoPuertaId",
                table: "Componente");

            migrationBuilder.CreateTable(
                name: "Puertas",
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
                    MaterialidadId = table.Column<long>(nullable: false),
                    TipoMarcoId = table.Column<long>(nullable: false),
                    Superficie = table.Column<double>(nullable: false),
                    MuroId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puertas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Puertas_Materialidades_MaterialidadId",
                        column: x => x.MaterialidadId,
                        principalTable: "Materialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Puertas_Muros_MuroId",
                        column: x => x.MuroId,
                        principalTable: "Muros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Puertas_Aislaciones_TipoMarcoId",
                        column: x => x.TipoMarcoId,
                        principalTable: "Aislaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Puertas_MaterialidadId",
                table: "Puertas",
                column: "MaterialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Puertas_MuroId",
                table: "Puertas",
                column: "MuroId");

            migrationBuilder.CreateIndex(
                name: "IX_Puertas_TipoMarcoId",
                table: "Puertas",
                column: "TipoMarcoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Puertas");

            migrationBuilder.AddColumn<double>(
                name: "Ancho",
                table: "Componente",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Largo",
                table: "Componente",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TipoPuertaId",
                table: "Componente",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Componente_TipoPuertaId",
                table: "Componente",
                column: "TipoPuertaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Componente_TipoPuertas_TipoPuertaId",
                table: "Componente",
                column: "TipoPuertaId",
                principalTable: "TipoPuertas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
