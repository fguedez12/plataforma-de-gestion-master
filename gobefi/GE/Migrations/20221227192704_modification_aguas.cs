using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class modification_aguas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdjuntoUrl",
                table: "Aguas",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "AdjuntoNombre",
                table: "Aguas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdjuntoNombre",
                table: "Aguas");

            migrationBuilder.AlterColumn<string>(
                name: "AdjuntoUrl",
                table: "Aguas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
