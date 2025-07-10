using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class justifica_residuos2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "JustificaResiduos",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "JustificacionResiduos",
                table: "Divisiones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JustificaResiduos",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "JustificacionResiduos",
                table: "Divisiones");
        }
    }
}
