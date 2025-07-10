using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class medidaDescripcionNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MedidaDescripcion",
                table: "Acciones",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MedidaDescripcion",
                table: "Acciones",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
