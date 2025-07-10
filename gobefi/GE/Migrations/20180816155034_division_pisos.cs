using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class division_pisos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pisos",
                table: "Divisiones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pisos",
                table: "Divisiones");
        }
    }
}
