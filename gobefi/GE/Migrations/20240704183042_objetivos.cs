using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class objetivos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ObjetivoId",
                table: "Brechas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Objetivos",
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
                    Titulo = table.Column<string>(nullable: false),
                    Descripcion = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objetivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Objetivos_DimensionBrechas_DimensionBrechaId",
                        column: x => x.DimensionBrechaId,
                        principalTable: "DimensionBrechas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brechas_ObjetivoId",
                table: "Brechas",
                column: "ObjetivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Objetivos_DimensionBrechaId",
                table: "Objetivos",
                column: "DimensionBrechaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brechas_Objetivos_ObjetivoId",
                table: "Brechas",
                column: "ObjetivoId",
                principalTable: "Objetivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brechas_Objetivos_ObjetivoId",
                table: "Brechas");

            migrationBuilder.DropTable(
                name: "Objetivos");

            migrationBuilder.DropIndex(
                name: "IX_Brechas_ObjetivoId",
                table: "Brechas");

            migrationBuilder.DropColumn(
                name: "ObjetivoId",
                table: "Brechas");
        }
    }
}
