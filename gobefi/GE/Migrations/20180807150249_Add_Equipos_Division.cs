using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class Add_Equipos_Division : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DivisionId",
                table: "Equipos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_DivisionId",
                table: "Equipos",
                column: "DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Divisiones_DivisionId",
                table: "Equipos",
                column: "DivisionId",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Divisiones_DivisionId",
                table: "Equipos");

            migrationBuilder.DropIndex(
                name: "IX_Equipos_DivisionId",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "Equipos");
        }
    }
}
