using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class jsutificaPapel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JustificaPapel",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReportaPapel",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JustificaPapel",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "ReportaPapel",
                table: "Divisiones");
        }
    }
}
