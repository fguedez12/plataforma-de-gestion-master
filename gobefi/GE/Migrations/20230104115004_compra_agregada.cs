using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class compra_agregada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompraAgredada",
                table: "Aguas",
                newName: "CompraAgregada");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompraAgregada",
                table: "Aguas",
                newName: "CompraAgredada");
        }
    }
}
