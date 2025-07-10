using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class AddFKClienteAndMedidor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<long>(
                name: "EnergeticoDivisionId",
                table: "NumeroClientes",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EnergeticoDivisionId",
                table: "Medidores",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_NumeroClientes_EnergeticoDivisionId",
                table: "NumeroClientes",
                column: "EnergeticoDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Medidores_EnergeticoDivisionId",
                table: "Medidores",
                column: "EnergeticoDivisionId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropIndex(
                name: "IX_NumeroClientes_EnergeticoDivisionId",
                table: "NumeroClientes");

            migrationBuilder.DropIndex(
                name: "IX_Medidores_EnergeticoDivisionId",
                table: "Medidores");

            migrationBuilder.DropColumn(
                name: "EnergeticoDivisionId",
                table: "NumeroClientes");

            migrationBuilder.DropColumn(
                name: "EnergeticoDivisionId",
                table: "Medidores");
       
        }
    }
}
