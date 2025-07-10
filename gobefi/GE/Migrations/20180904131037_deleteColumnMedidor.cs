using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class deleteColumnMedidor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Medidores_EnergeticoDivisiones_EnergeticoDivisionId1",
            //    table: "Medidores");

            migrationBuilder.DropIndex(
                name: "IX_Medidores_EnergeticoDivisionId",
                table: "Medidores");

            migrationBuilder.DropColumn(
                name: "EnergeticoDivisionId",
                table: "Medidores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EnergeticoDivisionId",
                table: "Medidores",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medidores_EnergeticoDivisionId",
                table: "Medidores",
                column: "EnergeticoDivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medidores_EnergeticoDivisiones_EnergeticoDivisionId",
                table: "Medidores",
                column: "EnergeticoDivisionId",
                principalTable: "EnergeticoDivisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
