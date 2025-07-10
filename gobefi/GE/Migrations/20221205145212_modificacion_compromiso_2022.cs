using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class modificacion_compromiso_2022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Compromiso2022",
                table: "Divisiones",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObservacionCompromiso2022",
                table: "Divisiones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObservacionCompromiso2022",
                table: "Divisiones");

            migrationBuilder.AlterColumn<bool>(
                name: "Compromiso2022",
                table: "Divisiones",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
