using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class RemoveFkUnidadMedidaMedidoresAndAddTablaTipoMedidores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medidores_UnidadesMedida_UnidadesMedidaId",
                table: "Medidores");

            migrationBuilder.DropIndex(
                name: "IX_Medidores_UnidadesMedidaId",
                table: "Medidores");

            migrationBuilder.DropColumn(
                name: "UnidadesMedidaId",
                table: "Medidores");

            migrationBuilder.CreateTable(
                name: "TipoMedidores",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    UnidadesMedidaId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMedidores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TipoMedidores_UnidadesMedida_UnidadesMedidaId",
                        column: x => x.UnidadesMedidaId,
                        principalTable: "UnidadesMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TipoMedidores_UnidadesMedidaId",
                table: "TipoMedidores",
                column: "UnidadesMedidaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoMedidores");

            migrationBuilder.AddColumn<long>(
                name: "UnidadesMedidaId",
                table: "Medidores",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Medidores_UnidadesMedidaId",
                table: "Medidores",
                column: "UnidadesMedidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medidores_UnidadesMedida_UnidadesMedidaId",
                table: "Medidores",
                column: "UnidadesMedidaId",
                principalTable: "UnidadesMedida",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
