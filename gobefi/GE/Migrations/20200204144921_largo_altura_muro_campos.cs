using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class largo_altura_muro_campos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Altura",
                table: "Muros",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Largo",
                table: "Muros",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Altura",
                table: "Muros");

            migrationBuilder.DropColumn(
                name: "Largo",
                table: "Muros");
        }
    }
}
