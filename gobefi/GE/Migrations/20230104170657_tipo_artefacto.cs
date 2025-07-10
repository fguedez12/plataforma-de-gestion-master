using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class tipo_artefacto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TipoArtefactoId1",
                table: "Artefactos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TipoArtefactos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoArtefactos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artefactos_TipoArtefactoId1",
                table: "Artefactos",
                column: "TipoArtefactoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Artefactos_TipoArtefactos_TipoArtefactoId1",
                table: "Artefactos",
                column: "TipoArtefactoId1",
                principalTable: "TipoArtefactos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artefactos_TipoArtefactos_TipoArtefactoId1",
                table: "Artefactos");

            migrationBuilder.DropTable(
                name: "TipoArtefactos");

            migrationBuilder.DropIndex(
                name: "IX_Artefactos_TipoArtefactoId1",
                table: "Artefactos");

            migrationBuilder.DropColumn(
                name: "TipoArtefactoId1",
                table: "Artefactos");
        }
    }
}
