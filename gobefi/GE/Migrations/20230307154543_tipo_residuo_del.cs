using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class tipo_residuo_del : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contenedores_TipoResiduos_TipoResiduoId",
                table: "Contenedores");

            migrationBuilder.DropTable(
                name: "TipoResiduos");

            migrationBuilder.DropIndex(
                name: "IX_Contenedores_TipoResiduoId",
                table: "Contenedores");

            migrationBuilder.DropColumn(
                name: "TipoResiduoId",
                table: "Residuos");

            migrationBuilder.DropColumn(
                name: "TipoResiduoId",
                table: "Contenedores");

            migrationBuilder.AddColumn<string>(
                name: "TipoResiduo",
                table: "Residuos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoResiduo",
                table: "Contenedores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoResiduo",
                table: "Residuos");

            migrationBuilder.DropColumn(
                name: "TipoResiduo",
                table: "Contenedores");

            migrationBuilder.AddColumn<long>(
                name: "TipoResiduoId",
                table: "Residuos",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TipoResiduoId",
                table: "Contenedores",
                nullable: false,
                defaultValue: 0L);

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

            migrationBuilder.CreateIndex(
                name: "IX_Contenedores_TipoResiduoId",
                table: "Contenedores",
                column: "TipoResiduoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contenedores_TipoResiduos_TipoResiduoId",
                table: "Contenedores",
                column: "TipoResiduoId",
                principalTable: "TipoResiduos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
