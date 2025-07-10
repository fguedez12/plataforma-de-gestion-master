using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class RelacionUsuarioComuna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Comunas_ComunaId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<long>(
                name: "ComunaId",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Comunas_ComunaId",
                table: "AspNetUsers",
                column: "ComunaId",
                principalTable: "Comunas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Comunas_ComunaId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<long>(
                name: "ComunaId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Comunas_ComunaId",
                table: "AspNetUsers",
                column: "ComunaId",
                principalTable: "Comunas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
