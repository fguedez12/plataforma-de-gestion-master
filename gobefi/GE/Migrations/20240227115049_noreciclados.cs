using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class noreciclados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "JustificaResiduosNoReciclados",
                table: "Divisiones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "JustificacionResiduosNoReciclados",
                table: "Divisiones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JustificaResiduosNoReciclados",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "JustificacionResiduosNoReciclados",
                table: "Divisiones");
        }
    }
}
