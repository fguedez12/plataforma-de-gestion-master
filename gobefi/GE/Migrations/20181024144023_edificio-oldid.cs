using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class edificiooldid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OldId1",
                table: "Edificios",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OldId1",
                table: "Divisiones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldId1",
                table: "Edificios");

            migrationBuilder.DropColumn(
                name: "OldId1",
                table: "Divisiones");
        }
    }
}
