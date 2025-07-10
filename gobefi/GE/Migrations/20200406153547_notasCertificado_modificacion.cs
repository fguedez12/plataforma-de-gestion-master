using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class notasCertificado_modificacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotasCertificados_AspNetUsers_UsuarioId",
                table: "NotasCertificados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotasCertificados",
                table: "NotasCertificados");

            migrationBuilder.RenameColumn(
                name: "Nota",
                table: "NotasCertificados",
                newName: "NotaFinal");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "NotasCertificados",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "NotasCertificados",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "NotasCertificados",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEntrega",
                table: "NotasCertificados",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NumeroSerie",
                table: "NotasCertificados",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotasCertificados",
                table: "NotasCertificados",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_NotasCertificados_CertificadoId",
                table: "NotasCertificados",
                column: "CertificadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotasCertificados_AspNetUsers_UsuarioId",
                table: "NotasCertificados",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotasCertificados_AspNetUsers_UsuarioId",
                table: "NotasCertificados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotasCertificados",
                table: "NotasCertificados");

            migrationBuilder.DropIndex(
                name: "IX_NotasCertificados_CertificadoId",
                table: "NotasCertificados");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "NotasCertificados");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "NotasCertificados");

            migrationBuilder.DropColumn(
                name: "FechaEntrega",
                table: "NotasCertificados");

            migrationBuilder.DropColumn(
                name: "NumeroSerie",
                table: "NotasCertificados");

            migrationBuilder.RenameColumn(
                name: "NotaFinal",
                table: "NotasCertificados",
                newName: "Nota");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "NotasCertificados",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotasCertificados",
                table: "NotasCertificados",
                columns: new[] { "CertificadoId", "UsuarioId" });

            migrationBuilder.AddForeignKey(
                name: "FK_NotasCertificados_AspNetUsers_UsuarioId",
                table: "NotasCertificados",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
