using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class resmas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resmas",
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
                    Agregada = table.Column<int>(nullable: false),
                    PapelReciclado = table.Column<bool>(nullable: false),
                    PapelRecicladoRango = table.Column<int>(nullable: false),
                    CantidadResmas = table.Column<int>(nullable: false),
                    FechaAdquisicion = table.Column<DateTime>(nullable: true),
                    AnioAdquisicion = table.Column<int>(nullable: false),
                    CostoEstimado = table.Column<int>(nullable: false),
                    DivisionId = table.Column<int>(nullable: false),
                    DivisionId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resmas_Divisiones_DivisionId1",
                        column: x => x.DivisionId1,
                        principalTable: "Divisiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resmas_DivisionId1",
                table: "Resmas",
                column: "DivisionId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resmas");
        }
    }
}
