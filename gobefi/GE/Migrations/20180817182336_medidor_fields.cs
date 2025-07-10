using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class medidor_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Fases",
                table: "Medidores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Smart",
                table: "Medidores",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fases",
                table: "Medidores");

            migrationBuilder.DropColumn(
                name: "Smart",
                table: "Medidores");
        }
    }
}
