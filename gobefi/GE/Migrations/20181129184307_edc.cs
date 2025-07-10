using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class edc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmpresasDistribuidoraComunas_Comunas_ComunaId",
                table: "EmpresasDistribuidoraComunas");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "EmpresasDistribuidoraComunas");

            migrationBuilder.AlterColumn<long>(
                name: "ComunaId",
                table: "EmpresasDistribuidoraComunas",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_EmpresasDistribuidoraComunas_Comunas_ComunaId",
                table: "EmpresasDistribuidoraComunas",
                column: "ComunaId",
                principalTable: "Comunas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmpresasDistribuidoraComunas_Comunas_ComunaId",
                table: "EmpresasDistribuidoraComunas");

            migrationBuilder.AlterColumn<long>(
                name: "ComunaId",
                table: "EmpresasDistribuidoraComunas",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EmpresaId",
                table: "EmpresasDistribuidoraComunas",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_EmpresasDistribuidoraComunas_Comunas_ComunaId",
                table: "EmpresasDistribuidoraComunas",
                column: "ComunaId",
                principalTable: "Comunas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
