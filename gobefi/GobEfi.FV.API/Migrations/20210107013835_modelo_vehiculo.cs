using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.FV.API.Migrations
{
    public partial class modelo_vehiculo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CilindradaId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "Comuna",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "MinisterioId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "Modelo",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "PropulsionId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "TipoId",
                table: "Vehiculos");

            migrationBuilder.RenameColumn(
                name: "Kmxlt",
                table: "Vehiculos",
                newName: "Kilometraje");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_ModeloId",
                table: "Vehiculos",
                column: "ModeloId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculos_Modelos_ModeloId",
                table: "Vehiculos",
                column: "ModeloId",
                principalTable: "Modelos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculos_Modelos_ModeloId",
                table: "Vehiculos");

            migrationBuilder.DropIndex(
                name: "IX_Vehiculos_ModeloId",
                table: "Vehiculos");

            migrationBuilder.RenameColumn(
                name: "Kilometraje",
                table: "Vehiculos",
                newName: "Kmxlt");

            migrationBuilder.AddColumn<long>(
                name: "CilindradaId",
                table: "Vehiculos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Comuna",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "Modelo",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PropulsionId",
                table: "Vehiculos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TipoId",
                table: "Vehiculos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
