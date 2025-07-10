using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class DpPComplete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DpP1",
                table: "Edificios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DpP2",
                table: "Edificios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DpP3",
                table: "Edificios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DpP4",
                table: "Edificios",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DpP1",
                table: "Edificios");

            migrationBuilder.DropColumn(
                name: "DpP2",
                table: "Edificios");

            migrationBuilder.DropColumn(
                name: "DpP3",
                table: "Edificios");

            migrationBuilder.DropColumn(
                name: "DpP4",
                table: "Edificios");
        }
    }
}
