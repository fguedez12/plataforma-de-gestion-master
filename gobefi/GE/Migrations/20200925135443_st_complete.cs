using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class st_complete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DpSt1",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DpSt2",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DpSt3",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DpSt4",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DpSt1",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "DpSt2",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "DpSt3",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "DpSt4",
                table: "Divisiones");
        }
    }
}
