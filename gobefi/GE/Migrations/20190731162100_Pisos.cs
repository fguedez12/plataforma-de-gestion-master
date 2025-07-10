using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class Pisos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Altura",
                table: "Componente");

            migrationBuilder.CreateTable(
                name: "Pisos",
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
                    TipoNivelPiso = table.Column<int>(nullable: false),
                    Superficie = table.Column<decimal>(nullable: false),
                    Altura = table.Column<decimal>(nullable: false),
                    NumeroPisoId = table.Column<long>(nullable: false),
                    DivisionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pisos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pisos_Divisiones_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Divisiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pisos_NumeroPisos_NumeroPisoId",
                        column: x => x.NumeroPisoId,
                        principalTable: "NumeroPisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pisos_DivisionId",
                table: "Pisos",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Pisos_NumeroPisoId",
                table: "Pisos",
                column: "NumeroPisoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pisos");

            migrationBuilder.AddColumn<double>(
                name: "Altura",
                table: "Componente",
                nullable: true);
        }
    }
}
