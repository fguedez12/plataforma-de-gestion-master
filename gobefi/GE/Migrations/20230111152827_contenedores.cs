using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class contenedores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoResiduos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoResiduos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contenedores",
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
                    Nombre = table.Column<string>(nullable: true),
                    UbicacionId = table.Column<int>(nullable: false),
                    Direccion = table.Column<string>(nullable: true),
                    NRecipientes = table.Column<int>(nullable: false),
                    TipoResiduoId = table.Column<long>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    DivisionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contenedores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contenedores_Divisiones_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Divisiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contenedores_TipoResiduos_TipoResiduoId",
                        column: x => x.TipoResiduoId,
                        principalTable: "TipoResiduos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contenedores_DivisionId",
                table: "Contenedores",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Contenedores_TipoResiduoId",
                table: "Contenedores",
                column: "TipoResiduoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contenedores");

            migrationBuilder.DropTable(
                name: "TipoResiduos");
        }
    }
}
