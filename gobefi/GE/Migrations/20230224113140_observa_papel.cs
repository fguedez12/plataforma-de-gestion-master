using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class observa_papel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReportaResiduos",
                table: "Divisiones",
                newName: "ObservaResiduos");

            migrationBuilder.RenameColumn(
                name: "ReportaPapel",
                table: "Divisiones",
                newName: "ObservaPapel");

            migrationBuilder.RenameColumn(
                name: "ReportaAgua",
                table: "Divisiones",
                newName: "ObservaAgua");

            migrationBuilder.RenameColumn(
                name: "JustificaResiduos",
                table: "Divisiones",
                newName: "ObservacionResiduos");

            migrationBuilder.RenameColumn(
                name: "JustificaPapel",
                table: "Divisiones",
                newName: "ObservacionPapel");

            migrationBuilder.RenameColumn(
                name: "JustificaAgua",
                table: "Divisiones",
                newName: "ObservacionAgua");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ObservacionResiduos",
                table: "Divisiones",
                newName: "JustificaResiduos");

            migrationBuilder.RenameColumn(
                name: "ObservacionPapel",
                table: "Divisiones",
                newName: "JustificaPapel");

            migrationBuilder.RenameColumn(
                name: "ObservacionAgua",
                table: "Divisiones",
                newName: "JustificaAgua");

            migrationBuilder.RenameColumn(
                name: "ObservaResiduos",
                table: "Divisiones",
                newName: "ReportaResiduos");

            migrationBuilder.RenameColumn(
                name: "ObservaPapel",
                table: "Divisiones",
                newName: "ReportaPapel");

            migrationBuilder.RenameColumn(
                name: "ObservaAgua",
                table: "Divisiones",
                newName: "ReportaAgua");
        }
    }
}
