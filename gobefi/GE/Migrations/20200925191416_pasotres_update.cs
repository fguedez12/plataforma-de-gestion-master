using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class pasotres_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Puertas_Materialidades_MaterialidadId",
                table: "Puertas");

            migrationBuilder.DropForeignKey(
                name: "FK_Puertas_Aislaciones_TipoMarcoId",
                table: "Puertas");

            migrationBuilder.DropForeignKey(
                name: "FK_Suelos_Aislaciones_AislacionId",
                table: "Suelos");

            migrationBuilder.DropForeignKey(
                name: "FK_Suelos_Materialidades_MaterialidadId",
                table: "Suelos");

            migrationBuilder.DropForeignKey(
                name: "FK_Suelos_Pisos_PisoId",
                table: "Suelos");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventanas_Materialidades_MaterialidadId",
                table: "Ventanas");

            migrationBuilder.AlterColumn<long>(
                name: "TipoMarcoId",
                table: "Ventanas",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "TipoCierreId",
                table: "Ventanas",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<double>(
                name: "Superficie",
                table: "Ventanas",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<long>(
                name: "MaterialidadId",
                table: "Ventanas",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "PisoId",
                table: "Suelos",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "MaterialidadId",
                table: "Suelos",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "AislacionId",
                table: "Suelos",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "TipoMarcoId",
                table: "Puertas",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<double>(
                name: "Superficie",
                table: "Puertas",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<long>(
                name: "MaterialidadId",
                table: "Puertas",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Puertas_Materialidades_MaterialidadId",
                table: "Puertas",
                column: "MaterialidadId",
                principalTable: "Materialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Puertas_Aislaciones_TipoMarcoId",
                table: "Puertas",
                column: "TipoMarcoId",
                principalTable: "Aislaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suelos_Aislaciones_AislacionId",
                table: "Suelos",
                column: "AislacionId",
                principalTable: "Aislaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suelos_Materialidades_MaterialidadId",
                table: "Suelos",
                column: "MaterialidadId",
                principalTable: "Materialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suelos_Pisos_PisoId",
                table: "Suelos",
                column: "PisoId",
                principalTable: "Pisos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventanas_Materialidades_MaterialidadId",
                table: "Ventanas",
                column: "MaterialidadId",
                principalTable: "Materialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Puertas_Materialidades_MaterialidadId",
                table: "Puertas");

            migrationBuilder.DropForeignKey(
                name: "FK_Puertas_Aislaciones_TipoMarcoId",
                table: "Puertas");

            migrationBuilder.DropForeignKey(
                name: "FK_Suelos_Aislaciones_AislacionId",
                table: "Suelos");

            migrationBuilder.DropForeignKey(
                name: "FK_Suelos_Materialidades_MaterialidadId",
                table: "Suelos");

            migrationBuilder.DropForeignKey(
                name: "FK_Suelos_Pisos_PisoId",
                table: "Suelos");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventanas_Materialidades_MaterialidadId",
                table: "Ventanas");

            migrationBuilder.AlterColumn<long>(
                name: "TipoMarcoId",
                table: "Ventanas",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TipoCierreId",
                table: "Ventanas",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Superficie",
                table: "Ventanas",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "MaterialidadId",
                table: "Ventanas",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PisoId",
                table: "Suelos",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "MaterialidadId",
                table: "Suelos",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AislacionId",
                table: "Suelos",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TipoMarcoId",
                table: "Puertas",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Superficie",
                table: "Puertas",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "MaterialidadId",
                table: "Puertas",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Puertas_Materialidades_MaterialidadId",
                table: "Puertas",
                column: "MaterialidadId",
                principalTable: "Materialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Puertas_Aislaciones_TipoMarcoId",
                table: "Puertas",
                column: "TipoMarcoId",
                principalTable: "Aislaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suelos_Aislaciones_AislacionId",
                table: "Suelos",
                column: "AislacionId",
                principalTable: "Aislaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suelos_Materialidades_MaterialidadId",
                table: "Suelos",
                column: "MaterialidadId",
                principalTable: "Materialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suelos_Pisos_PisoId",
                table: "Suelos",
                column: "PisoId",
                principalTable: "Pisos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventanas_Materialidades_MaterialidadId",
                table: "Ventanas",
                column: "MaterialidadId",
                principalTable: "Materialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
