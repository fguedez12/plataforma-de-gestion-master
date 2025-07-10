using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class documentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documento_TipoDocumentos_TipoDocumentoId",
                table: "Documento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documento",
                table: "Documento");

            migrationBuilder.RenameTable(
                name: "Documento",
                newName: "Documentos");

            migrationBuilder.RenameIndex(
                name: "IX_Documento_TipoDocumentoId",
                table: "Documentos",
                newName: "IX_Documentos_TipoDocumentoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documentos",
                table: "Documentos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_TipoDocumentos_TipoDocumentoId",
                table: "Documentos",
                column: "TipoDocumentoId",
                principalTable: "TipoDocumentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_TipoDocumentos_TipoDocumentoId",
                table: "Documentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documentos",
                table: "Documentos");

            migrationBuilder.RenameTable(
                name: "Documentos",
                newName: "Documento");

            migrationBuilder.RenameIndex(
                name: "IX_Documentos_TipoDocumentoId",
                table: "Documento",
                newName: "IX_Documento_TipoDocumentoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documento",
                table: "Documento",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documento_TipoDocumentos_TipoDocumentoId",
                table: "Documento",
                column: "TipoDocumentoId",
                principalTable: "TipoDocumentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
