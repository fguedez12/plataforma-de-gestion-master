using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class add_comuna_null_edificio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Edificios_Comunas_ComunaId",
                table: "Edificios");

            migrationBuilder.AlterColumn<long>(
                name: "ComunaId",
                table: "Edificios",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Edificios_Comunas_ComunaId",
                table: "Edificios",
                column: "ComunaId",
                principalTable: "Comunas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Edificios_Comunas_ComunaId",
                table: "Edificios");

            migrationBuilder.AlterColumn<long>(
                name: "ComunaId",
                table: "Edificios",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Edificios_Comunas_ComunaId",
                table: "Edificios",
                column: "ComunaId",
                principalTable: "Comunas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
