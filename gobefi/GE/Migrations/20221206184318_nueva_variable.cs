using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class nueva_variable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Compromiso2022",
                table: "Divisiones",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstadoCompromiso2022",
                table: "Divisiones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoCompromiso2022",
                table: "Divisiones");

            migrationBuilder.AlterColumn<int>(
                name: "Compromiso2022",
                table: "Divisiones",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
