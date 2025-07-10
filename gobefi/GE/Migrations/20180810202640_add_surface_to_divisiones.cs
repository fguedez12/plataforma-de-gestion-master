using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class add_surface_to_divisiones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Superficie",
                table: "Divisiones",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Superficie",
                table: "Divisiones");
        }
    }
}
