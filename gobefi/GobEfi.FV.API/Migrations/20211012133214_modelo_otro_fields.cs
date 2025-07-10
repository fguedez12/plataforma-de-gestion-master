using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.FV.API.Migrations
{
    public partial class modelo_otro_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculos_Modelos_ModeloId",
                table: "Vehiculos");

            migrationBuilder.AlterColumn<long>(
                name: "ModeloId",
                table: "Vehiculos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Carroceria",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cilindrada",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CombustibleId",
                table: "Vehiculos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModeloOtro",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PropulsionId",
                table: "Vehiculos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Traccion",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Transmision",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Combustibles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combustibles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Propulsiones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propulsiones", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_CombustibleId",
                table: "Vehiculos",
                column: "CombustibleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_PropulsionId",
                table: "Vehiculos",
                column: "PropulsionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculos_Combustibles_CombustibleId",
                table: "Vehiculos",
                column: "CombustibleId",
                principalTable: "Combustibles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculos_Modelos_ModeloId",
                table: "Vehiculos",
                column: "ModeloId",
                principalTable: "Modelos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculos_Propulsiones_PropulsionId",
                table: "Vehiculos",
                column: "PropulsionId",
                principalTable: "Propulsiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculos_Combustibles_CombustibleId",
                table: "Vehiculos");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculos_Modelos_ModeloId",
                table: "Vehiculos");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculos_Propulsiones_PropulsionId",
                table: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Combustibles");

            migrationBuilder.DropTable(
                name: "Propulsiones");

            migrationBuilder.DropIndex(
                name: "IX_Vehiculos_CombustibleId",
                table: "Vehiculos");

            migrationBuilder.DropIndex(
                name: "IX_Vehiculos_PropulsionId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "Carroceria",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "Cilindrada",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "CombustibleId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "ModeloOtro",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "PropulsionId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "Traccion",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "Transmision",
                table: "Vehiculos");

            migrationBuilder.AlterColumn<long>(
                name: "ModeloId",
                table: "Vehiculos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculos_Modelos_ModeloId",
                table: "Vehiculos",
                column: "ModeloId",
                principalTable: "Modelos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
