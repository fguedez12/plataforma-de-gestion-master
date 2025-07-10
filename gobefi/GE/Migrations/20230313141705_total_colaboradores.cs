using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class total_colaboradores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalColaboradoresCapacitados",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalColaboradoresConcientizados",
                table: "Documentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalColaboradoresCapacitados",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "TotalColaboradoresConcientizados",
                table: "Documentos");
        }
    }
}
