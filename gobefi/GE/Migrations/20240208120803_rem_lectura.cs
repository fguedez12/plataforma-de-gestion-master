using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class rem_lectura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Escritura",
                table: "PermisosBackEnd");

            migrationBuilder.DropColumn(
                name: "Lectura",
                table: "PermisosBackEnd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Escritura",
                table: "PermisosBackEnd",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Lectura",
                table: "PermisosBackEnd",
                nullable: false,
                defaultValue: false);
        }
    }
}
