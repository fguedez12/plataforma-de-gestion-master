using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class SeCambiaTipoDeDatoRolId_SeExtiendeSubMenuConURL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permisos_AspNetRoles_RolId1",
                table: "Permisos");

            migrationBuilder.DropIndex(
                name: "IX_Permisos_RolId1",
                table: "Permisos");

            migrationBuilder.DropColumn(
                name: "RolId1",
                table: "Permisos");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "SubMenu",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RolId",
                table: "Permisos",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_RolId",
                table: "Permisos",
                column: "RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permisos_AspNetRoles_RolId",
                table: "Permisos",
                column: "RolId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permisos_AspNetRoles_RolId",
                table: "Permisos");

            migrationBuilder.DropIndex(
                name: "IX_Permisos_RolId",
                table: "Permisos");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "SubMenu");

            migrationBuilder.AlterColumn<long>(
                name: "RolId",
                table: "Permisos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RolId1",
                table: "Permisos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_RolId1",
                table: "Permisos",
                column: "RolId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Permisos_AspNetRoles_RolId1",
                table: "Permisos",
                column: "RolId1",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
