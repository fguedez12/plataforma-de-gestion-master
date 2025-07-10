using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class Pisos_tiponivelpisoId_remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoNivelPiso",
                table: "Pisos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoNivelPiso",
                table: "Pisos",
                nullable: false,
                defaultValue: 0);
        }
    }
}
