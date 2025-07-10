using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class CreacionMenuPanel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MenuPanelId",
                table: "Menu",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "MenuPanel",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    TagId = table.Column<string>(nullable: true),
                    TagHref = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPanel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menu_MenuPanelId",
                table: "Menu",
                column: "MenuPanelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_MenuPanel_MenuPanelId",
                table: "Menu",
                column: "MenuPanelId",
                principalTable: "MenuPanel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_MenuPanel_MenuPanelId",
                table: "Menu");

            migrationBuilder.DropTable(
                name: "MenuPanel");

            migrationBuilder.DropIndex(
                name: "IX_Menu_MenuPanelId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "MenuPanelId",
                table: "Menu");
        }
    }
}
