using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class justifica_residuos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JustificaResiduos",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReportaResiduos",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JustificaResiduos",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "ReportaResiduos",
                table: "Divisiones");
        }
    }
}
