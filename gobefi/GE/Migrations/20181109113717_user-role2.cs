using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class userrole2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UsuarioId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UsuarioId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UsuarioId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UsuarioId",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserTokens_UsuarioId",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserLogins_UsuarioId",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserClaims_UsuarioId",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "AspNetUserClaims");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "AspNetUserRoles",
                newName: "UsuarioId1");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_UsuarioId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_UsuarioId1");

            migrationBuilder.AddColumn<string>(
                name: "RolId1",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RolId1",
                table: "AspNetUserRoles",
                column: "RolId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RolId1",
                table: "AspNetUserRoles",
                column: "RolId1",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UsuarioId1",
                table: "AspNetUserRoles",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RolId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UsuarioId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_RolId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "RolId1",
                table: "AspNetUserRoles");

            migrationBuilder.RenameColumn(
                name: "UsuarioId1",
                table: "AspNetUserRoles",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_UsuarioId1",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_UsuarioId");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "AspNetUserLogins",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "AspNetUserClaims",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserTokens_UsuarioId",
                table: "AspNetUserTokens",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UsuarioId",
                table: "AspNetUserLogins",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UsuarioId",
                table: "AspNetUserClaims",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UsuarioId",
                table: "AspNetUserClaims",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UsuarioId",
                table: "AspNetUserLogins",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UsuarioId",
                table: "AspNetUserRoles",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UsuarioId",
                table: "AspNetUserTokens",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
