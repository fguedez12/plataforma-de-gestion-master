using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class ventanaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Componente_TipoVentanas_TipoVentanaId",
                table: "Componente");

            migrationBuilder.DropIndex(
                name: "IX_Componente_TipoVentanaId",
                table: "Componente");

            migrationBuilder.DropColumn(
                name: "Alto",
                table: "Componente");

            migrationBuilder.DropColumn(
                name: "Ventana_Largo",
                table: "Componente");

            migrationBuilder.DropColumn(
                name: "TipoVentanaId",
                table: "Componente");

            migrationBuilder.CreateTable(
                name: "Ventanas",
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
                    TipoCierreId = table.Column<long>(nullable: false),
                    TipoMarcoId = table.Column<long>(nullable: false),
                    PisoId = table.Column<long>(nullable: false),
                    Supercifie = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventanas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ventanas_Materialidades_MaterialidadId",
                        column: x => x.MaterialidadId,
                        principalTable: "Materialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventanas_Pisos_PisoId",
                        column: x => x.PisoId,
                        principalTable: "Pisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventanas_Aislaciones_TipoCierreId",
                        column: x => x.TipoCierreId,
                        principalTable: "Aislaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ventanas_Aislaciones_TipoMarcoId",
                        column: x => x.TipoMarcoId,
                        principalTable: "Aislaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ventanas_MaterialidadId",
                table: "Ventanas",
                column: "MaterialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventanas_PisoId",
                table: "Ventanas",
                column: "PisoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventanas_TipoCierreId",
                table: "Ventanas",
                column: "TipoCierreId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventanas_TipoMarcoId",
                table: "Ventanas",
                column: "TipoMarcoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ventanas");

            migrationBuilder.AddColumn<double>(
                name: "Alto",
                table: "Componente",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Ventana_Largo",
                table: "Componente",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TipoVentanaId",
                table: "Componente",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Componente_TipoVentanaId",
                table: "Componente",
                column: "TipoVentanaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Componente_TipoVentanas_TipoVentanaId",
                table: "Componente",
                column: "TipoVentanaId",
                principalTable: "TipoVentanas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
