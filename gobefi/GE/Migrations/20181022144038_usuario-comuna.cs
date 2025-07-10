using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class usuariocomuna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Region",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "ComunaId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ComunaId1",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ComunaId1",
                table: "AspNetUsers",
                column: "ComunaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Comunas_ComunaId1",
                table: "AspNetUsers",
                column: "ComunaId1",
                principalTable: "Comunas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Comunas_ComunaId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ComunaId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ComunaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ComunaId1",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
