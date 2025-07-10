using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class pisos_suelos_cardinalidad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Suelos_PisoId",
                table: "Suelos");

            migrationBuilder.CreateIndex(
                name: "IX_Suelos_PisoId",
                table: "Suelos",
                column: "PisoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Suelos_PisoId",
                table: "Suelos");

            migrationBuilder.CreateIndex(
                name: "IX_Suelos_PisoId",
                table: "Suelos",
                column: "PisoId",
                unique: true);
        }
    }
}
