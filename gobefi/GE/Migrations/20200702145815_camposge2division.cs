using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class camposge2division : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GeVersion",
                table: "Divisiones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProvinciaId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RegionId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TipoAdministracionId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TipoInmueble",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Divisiones_ParentId",
                table: "Divisiones",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Divisiones_ProvinciaId",
                table: "Divisiones",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Divisiones_RegionId",
                table: "Divisiones",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Divisiones_Divisiones_ParentId",
                table: "Divisiones",
                column: "ParentId",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Divisiones_Provincias_ProvinciaId",
                table: "Divisiones",
                column: "ProvinciaId",
                principalTable: "Provincias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Divisiones_Regiones_RegionId",
                table: "Divisiones",
                column: "RegionId",
                principalTable: "Regiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Divisiones_Divisiones_ParentId",
                table: "Divisiones");

            migrationBuilder.DropForeignKey(
                name: "FK_Divisiones_Provincias_ProvinciaId",
                table: "Divisiones");

            migrationBuilder.DropForeignKey(
                name: "FK_Divisiones_Regiones_RegionId",
                table: "Divisiones");

            migrationBuilder.DropIndex(
                name: "IX_Divisiones_ParentId",
                table: "Divisiones");

            migrationBuilder.DropIndex(
                name: "IX_Divisiones_ProvinciaId",
                table: "Divisiones");

            migrationBuilder.DropIndex(
                name: "IX_Divisiones_RegionId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "GeVersion",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "ProvinciaId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "TipoAdministracionId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "TipoInmueble",
                table: "Divisiones");
        }
    }
}
