using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class checkPolE2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Consultiva",
                table: "Documentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "E1O1RT2",
                table: "Documentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Consultiva",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "E1O1RT2",
                table: "Documentos");
        }
    }
}
