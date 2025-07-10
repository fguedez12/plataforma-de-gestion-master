using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class justifica_agua : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JustificaAgua",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReportaAgua",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JustificaAgua",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "ReportaAgua",
                table: "Divisiones");
        }
    }
}
