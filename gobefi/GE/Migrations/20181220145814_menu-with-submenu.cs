using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class menuwithsubmenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_MenuPanel_MenuPanelId",
                table: "Menu");

            migrationBuilder.AlterColumn<int>(
                name: "Orden",
                table: "Menu",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "MenuPanelId",
                table: "Menu",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "MenuId",
                table: "Menu",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParentMenu",
                table: "Menu",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menu_MenuId",
                table: "Menu",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Menu_MenuId",
                table: "Menu",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_MenuPanel_MenuPanelId",
                table: "Menu",
                column: "MenuPanelId",
                principalTable: "MenuPanel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Menu_MenuId",
                table: "Menu");

            migrationBuilder.DropForeignKey(
                name: "FK_Menu_MenuPanel_MenuPanelId",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Menu_MenuId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "ParentMenu",
                table: "Menu");

            migrationBuilder.AlterColumn<int>(
                name: "Orden",
                table: "Menu",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "MenuPanelId",
                table: "Menu",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_MenuPanel_MenuPanelId",
                table: "Menu",
                column: "MenuPanelId",
                principalTable: "MenuPanel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
