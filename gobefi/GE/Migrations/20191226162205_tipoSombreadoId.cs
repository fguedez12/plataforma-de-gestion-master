using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class tipoSombreadoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TipoSombreadoId",
                table: "Muros",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Muros_TipoSombreadoId",
                table: "Muros",
                column: "TipoSombreadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Muros_TipoSombreados_TipoSombreadoId",
                table: "Muros",
                column: "TipoSombreadoId",
                principalTable: "TipoSombreados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Muros_TipoSombreados_TipoSombreadoId",
                table: "Muros");

            migrationBuilder.DropIndex(
                name: "IX_Muros_TipoSombreadoId",
                table: "Muros");

            migrationBuilder.DropColumn(
                name: "TipoSombreadoId",
                table: "Muros");
        }
    }
}
