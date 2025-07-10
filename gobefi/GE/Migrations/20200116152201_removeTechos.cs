using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class removeTechos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Componente_TipoTechos_TipoTechoId",
                table: "Componente");

            migrationBuilder.DropIndex(
                name: "IX_Componente_TipoTechoId",
                table: "Componente");

            migrationBuilder.DropColumn(
                name: "Techo_Ancho",
                table: "Componente");

            migrationBuilder.DropColumn(
                name: "Techo_Largo",
                table: "Componente");

            migrationBuilder.DropColumn(
                name: "TipoTechoId",
                table: "Componente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Techo_Ancho",
                table: "Componente",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Techo_Largo",
                table: "Componente",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TipoTechoId",
                table: "Componente",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Componente_TipoTechoId",
                table: "Componente",
                column: "TipoTechoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Componente_TipoTechos_TipoTechoId",
                table: "Componente",
                column: "TipoTechoId",
                principalTable: "TipoTechos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
