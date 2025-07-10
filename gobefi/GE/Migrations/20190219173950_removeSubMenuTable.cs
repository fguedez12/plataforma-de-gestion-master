using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class removeSubMenuTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permisos_SubMenu_SubMenuId",
                table: "Permisos");

            migrationBuilder.DropTable(
                name: "SubMenu");

            migrationBuilder.DropIndex(
                name: "IX_Permisos_SubMenuId",
                table: "Permisos");

            migrationBuilder.DropColumn(
                name: "SubMenuId",
                table: "Permisos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SubMenuId",
                table: "Permisos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubMenu",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Action = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Icono = table.Column<string>(nullable: true),
                    MenuId = table.Column<long>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubMenu_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_SubMenuId",
                table: "Permisos",
                column: "SubMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenu_MenuId",
                table: "SubMenu",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permisos_SubMenu_SubMenuId",
                table: "Permisos",
                column: "SubMenuId",
                principalTable: "SubMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
