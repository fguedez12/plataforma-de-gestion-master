using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class fachadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "FachadaEste",
                table: "Muros",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "FachadaNorte",
                table: "Muros",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "FachadaOeste",
                table: "Muros",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "FachadaSur",
                table: "Muros",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FachadaEste",
                table: "Muros");

            migrationBuilder.DropColumn(
                name: "FachadaNorte",
                table: "Muros");

            migrationBuilder.DropColumn(
                name: "FachadaOeste",
                table: "Muros");

            migrationBuilder.DropColumn(
                name: "FachadaSur",
                table: "Muros");
        }
    }
}
