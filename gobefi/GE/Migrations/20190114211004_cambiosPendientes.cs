using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class cambiosPendientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmpresasDistribuidoraComunas_Comunas_ComunaId",
                table: "EmpresasDistribuidoraComunas");

            migrationBuilder.DropForeignKey(
                name: "FK_EmpresasDistribuidoraComunas_EmpresaDistribuidoras_EmpresaDistribuidoraId",
                table: "EmpresasDistribuidoraComunas");

            migrationBuilder.AlterColumn<long>(
                name: "EmpresaDistribuidoraId",
                table: "EmpresasDistribuidoraComunas",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ComunaId",
                table: "EmpresasDistribuidoraComunas",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmpresasDistribuidoraComunas_Comunas_ComunaId",
                table: "EmpresasDistribuidoraComunas",
                column: "ComunaId",
                principalTable: "Comunas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmpresasDistribuidoraComunas_EmpresaDistribuidoras_EmpresaDistribuidoraId",
                table: "EmpresasDistribuidoraComunas",
                column: "EmpresaDistribuidoraId",
                principalTable: "EmpresaDistribuidoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmpresasDistribuidoraComunas_Comunas_ComunaId",
                table: "EmpresasDistribuidoraComunas");

            migrationBuilder.DropForeignKey(
                name: "FK_EmpresasDistribuidoraComunas_EmpresaDistribuidoras_EmpresaDistribuidoraId",
                table: "EmpresasDistribuidoraComunas");

            migrationBuilder.AlterColumn<long>(
                name: "EmpresaDistribuidoraId",
                table: "EmpresasDistribuidoraComunas",
                nullable: true,
                oldClrType: typeof(long));

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

            migrationBuilder.AddForeignKey(
                name: "FK_EmpresasDistribuidoraComunas_EmpresaDistribuidoras_EmpresaDistribuidoraId",
                table: "EmpresasDistribuidoraComunas",
                column: "EmpresaDistribuidoraId",
                principalTable: "EmpresaDistribuidoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
