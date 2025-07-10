using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class integrante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TieneVehiculo",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Integrantes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Rol = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Marca = table.Column<bool>(nullable: false),
                    ListaIntegranteId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Integrantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Integrantes_Documentos_ListaIntegranteId",
                        column: x => x.ListaIntegranteId,
                        principalTable: "Documentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Integrantes_ListaIntegranteId",
                table: "Integrantes",
                column: "ListaIntegranteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Integrantes");

            migrationBuilder.DropColumn(
                name: "TieneVehiculo",
                table: "Divisiones");
        }
    }
}
