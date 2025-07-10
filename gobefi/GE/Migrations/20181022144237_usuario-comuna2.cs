using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class usuariocomuna2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Comunas_ComunaId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ComunaId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ComunaId1",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<long>(
                name: "ComunaId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ComunaId",
                table: "AspNetUsers",
                column: "ComunaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Comunas_ComunaId",
                table: "AspNetUsers",
                column: "ComunaId",
                principalTable: "Comunas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Comunas_ComunaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ComunaId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "ComunaId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

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
    }
}
