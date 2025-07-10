using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class SeAgregaEntidadSexo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SexoId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sexo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SexoId",
                table: "AspNetUsers",
                column: "SexoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Sexo_SexoId",
                table: "AspNetUsers",
                column: "SexoId",
                principalTable: "Sexo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Sexo_SexoId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Sexo");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SexoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SexoId",
                table: "AspNetUsers");
        }
    }
}
