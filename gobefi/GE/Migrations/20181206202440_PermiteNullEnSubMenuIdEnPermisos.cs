using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class PermiteNullEnSubMenuIdEnPermisos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SubMenuId",
                table: "Permisos",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SubMenuId",
                table: "Permisos",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
