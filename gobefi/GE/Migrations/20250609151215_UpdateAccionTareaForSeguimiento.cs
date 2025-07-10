using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class UpdateAccionTareaForSeguimiento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescripcionTareaEjecutada",
                table: "PlanGestion_Tareas",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CostoAsociado",
                table: "Acciones",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstadoAvance",
                table: "Acciones",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "Acciones",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescripcionTareaEjecutada",
                table: "PlanGestion_Tareas");

            migrationBuilder.DropColumn(
                name: "CostoAsociado",
                table: "Acciones");

            migrationBuilder.DropColumn(
                name: "EstadoAvance",
                table: "Acciones");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "Acciones");
        }
    }
}
