using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class changeNameParametroMedicion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoMedidores");

            migrationBuilder.CreateTable(
                name: "ParametrosMedicion",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    UnidadesMedidaId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametrosMedicion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParametrosMedicion_UnidadesMedida_UnidadesMedidaId",
                        column: x => x.UnidadesMedidaId,
                        principalTable: "UnidadesMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParametrosMedicion_UnidadesMedidaId",
                table: "ParametrosMedicion",
                column: "UnidadesMedidaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParametrosMedicion");

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
    }
}
