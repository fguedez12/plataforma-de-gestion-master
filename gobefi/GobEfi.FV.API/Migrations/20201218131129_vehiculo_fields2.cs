using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.FV.API.Migrations
{
    public partial class vehiculo_fields2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CilindradaId",
                table: "Vehiculos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MarcaId",
                table: "Vehiculos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MinisterioId",
                table: "Vehiculos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PropulsionId",
                table: "Vehiculos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ServicioId",
                table: "Vehiculos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TipoId",
                table: "Vehiculos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TipoPropiedadId",
                table: "Vehiculos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CilindradaId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "MinisterioId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "PropulsionId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "ServicioId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "TipoId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "TipoPropiedadId",
                table: "Vehiculos");
        }
    }
}
