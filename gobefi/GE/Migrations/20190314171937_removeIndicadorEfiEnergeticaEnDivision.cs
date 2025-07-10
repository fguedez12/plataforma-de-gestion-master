using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class removeIndicadorEfiEnergeticaEnDivision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportaIndicadorEfiEnergetica",
                table: "Divisiones");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ReportaIndicadorEfiEnergetica",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);
        }
    }
}
