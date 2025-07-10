using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class new_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Destruccion",
                table: "Documentos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Donacion",
                table: "Documentos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Reparacion",
                table: "Documentos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destruccion",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "Donacion",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "Reparacion",
                table: "Documentos");
        }
    }
}
