using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class relacion_tipo_documento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ActasComite",
                table: "ActasComite");

            migrationBuilder.RenameTable(
                name: "ActasComite",
                newName: "Documento");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Documento",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TipoDocumentoId",
                table: "Documento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documento",
                table: "Documento",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_TipoDocumentoId",
                table: "Documento",
                column: "TipoDocumentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documento_TipoDocumentos_TipoDocumentoId",
                table: "Documento",
                column: "TipoDocumentoId",
                principalTable: "TipoDocumentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documento_TipoDocumentos_TipoDocumentoId",
                table: "Documento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documento",
                table: "Documento");

            migrationBuilder.DropIndex(
                name: "IX_Documento_TipoDocumentoId",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "TipoDocumentoId",
                table: "Documento");

            migrationBuilder.RenameTable(
                name: "Documento",
                newName: "ActasComite");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActasComite",
                table: "ActasComite",
                column: "Id");
        }
    }
}
