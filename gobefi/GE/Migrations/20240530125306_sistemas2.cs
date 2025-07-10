using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class sistemas2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColectorSfId",
                table: "Divisiones");

            migrationBuilder.AddColumn<int>(
                name: "MantColectores",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MantSfv",
                table: "Divisiones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MantColectores",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "MantSfv",
                table: "Divisiones");

            migrationBuilder.AddColumn<long>(
                name: "ColectorSfId",
                table: "Divisiones",
                nullable: true);
        }
    }
}
