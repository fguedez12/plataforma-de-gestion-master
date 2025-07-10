using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class cant_reciclada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PapelReciclado",
                table: "Resmas");

            migrationBuilder.AddColumn<int>(
                name: "CantidadResmasRecicladas",
                table: "Resmas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadResmasRecicladas",
                table: "Resmas");

            migrationBuilder.AddColumn<bool>(
                name: "PapelReciclado",
                table: "Resmas",
                nullable: true);
        }
    }
}
