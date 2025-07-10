using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class compromiso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Compromiso2022",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Justificacion",
                table: "Divisiones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Compromiso2022",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "Justificacion",
                table: "Divisiones");
        }
    }
}
