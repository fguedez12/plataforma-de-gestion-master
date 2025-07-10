using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class murosTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Muros",
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
                    Frontis = table.Column<bool>(nullable: false),
                    Azimut = table.Column<float>(nullable: false),
                    Distancia = table.Column<float>(nullable: false),
                    Latitud = table.Column<float>(nullable: false),
                    Longitud = table.Column<float>(nullable: false),
                    Orientacion = table.Column<string>(nullable: true),
                    TipoMuro = table.Column<string>(nullable: true),
                    Vanos = table.Column<float>(nullable: false),
                    PisoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Muros_Pisos_PisoId",
                        column: x => x.PisoId,
                        principalTable: "Pisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Muros_PisoId",
                table: "Muros",
                column: "PisoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Muros");
        }
    }
}
