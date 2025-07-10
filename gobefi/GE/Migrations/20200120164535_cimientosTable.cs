using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class cimientosTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Componente_Divisiones_DivisionId",
                table: "Componente");

            migrationBuilder.DropForeignKey(
                name: "FK_Componente_TipoAislaciones_TipoAislacionId",
                table: "Componente");

            migrationBuilder.DropForeignKey(
                name: "FK_Componente_TipoMateriales_TipoMaterialId",
                table: "Componente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Componente",
                table: "Componente");

            migrationBuilder.DropIndex(
                name: "IX_Componente_DivisionId",
                table: "Componente");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Componente");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "Componente");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Componente");

            migrationBuilder.DropColumn(
                name: "Superficie",
                table: "Componente");

            migrationBuilder.DropColumn(
                name: "TieneAislacion",
                table: "Componente");

            migrationBuilder.RenameTable(
                name: "Componente",
                newName: "Cimientos");

            migrationBuilder.RenameColumn(
                name: "TipoMaterialId",
                table: "Cimientos",
                newName: "MaterialidadId");

            migrationBuilder.RenameColumn(
                name: "TipoAislacionId",
                table: "Cimientos",
                newName: "EdificioId");



            migrationBuilder.AddPrimaryKey(
                name: "PK_Cimientos",
                table: "Cimientos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cimientos_Edificios_EdificioId",
                table: "Cimientos",
                column: "EdificioId",
                principalTable: "Edificios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cimientos_Materialidades_MaterialidadId",
                table: "Cimientos",
                column: "MaterialidadId",
                principalTable: "Materialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cimientos_Edificios_EdificioId",
                table: "Cimientos");

            migrationBuilder.DropForeignKey(
                name: "FK_Cimientos_Materialidades_MaterialidadId",
                table: "Cimientos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cimientos",
                table: "Cimientos");

            migrationBuilder.RenameTable(
                name: "Cimientos",
                newName: "Componente");

            migrationBuilder.RenameColumn(
                name: "MaterialidadId",
                table: "Componente",
                newName: "TipoMaterialId");

            migrationBuilder.RenameColumn(
                name: "EdificioId",
                table: "Componente",
                newName: "TipoAislacionId");

            migrationBuilder.RenameIndex(
                name: "IX_Cimientos_MaterialidadId",
                table: "Componente",
                newName: "IX_Componente_TipoMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_Cimientos_EdificioId",
                table: "Componente",
                newName: "IX_Componente_TipoAislacionId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Componente",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "DivisionId",
                table: "Componente",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Componente",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Superficie",
                table: "Componente",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "TieneAislacion",
                table: "Componente",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Componente",
                table: "Componente",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Componente_DivisionId",
                table: "Componente",
                column: "DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Componente_Divisiones_DivisionId",
                table: "Componente",
                column: "DivisionId",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Componente_TipoAislaciones_TipoAislacionId",
                table: "Componente",
                column: "TipoAislacionId",
                principalTable: "TipoAislaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Componente_TipoMateriales_TipoMaterialId",
                table: "Componente",
                column: "TipoMaterialId",
                principalTable: "TipoMateriales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
