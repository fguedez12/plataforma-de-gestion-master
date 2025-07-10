using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class deleteColumnDivisiononMedidor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK_Medidores_Divisiones_DivisionId1",
            table: "Medidores");

            migrationBuilder.DropIndex(
                name: "IX_Medidores_DivisionId1",
                table: "Medidores");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "Medidores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
