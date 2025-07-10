using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class add_surface_to_divisiones2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Superficie",
                table: "Divisiones",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Superficie",
                table: "Divisiones",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
