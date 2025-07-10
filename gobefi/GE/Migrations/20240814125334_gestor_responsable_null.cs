using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class gestor_responsable_null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GestorRespnsableId",
                table: "Acciones",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GestorRespnsableId",
                table: "Acciones",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
