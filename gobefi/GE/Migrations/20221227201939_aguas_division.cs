using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class aguas_division : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DivisionId",
                table: "Aguas",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Aguas_DivisionId",
                table: "Aguas",
                column: "DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aguas_Divisiones_DivisionId",
                table: "Aguas",
                column: "DivisionId",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aguas_Divisiones_DivisionId",
                table: "Aguas");

            migrationBuilder.DropIndex(
                name: "IX_Aguas_DivisionId",
                table: "Aguas");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "Aguas");
        }
    }
}
