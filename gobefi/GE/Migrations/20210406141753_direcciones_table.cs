using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class direcciones_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Direccion_Comunas_ComunaId",
                table: "Direccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Direccion_Provincias_ProvinciaId",
                table: "Direccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Direccion_Regiones_RegionId",
                table: "Direccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Divisiones_Direccion_DireccionInmuebleId",
                table: "Divisiones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Direccion",
                table: "Direccion");

            migrationBuilder.RenameTable(
                name: "Direccion",
                newName: "Direcciones");

            migrationBuilder.RenameIndex(
                name: "IX_Direccion_RegionId",
                table: "Direcciones",
                newName: "IX_Direcciones_RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Direccion_ProvinciaId",
                table: "Direcciones",
                newName: "IX_Direcciones_ProvinciaId");

            migrationBuilder.RenameIndex(
                name: "IX_Direccion_ComunaId",
                table: "Direcciones",
                newName: "IX_Direcciones_ComunaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Direcciones",
                table: "Direcciones",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Direcciones_Comunas_ComunaId",
                table: "Direcciones",
                column: "ComunaId",
                principalTable: "Comunas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Direcciones_Provincias_ProvinciaId",
                table: "Direcciones",
                column: "ProvinciaId",
                principalTable: "Provincias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Direcciones_Regiones_RegionId",
                table: "Direcciones",
                column: "RegionId",
                principalTable: "Regiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Divisiones_Direcciones_DireccionInmuebleId",
                table: "Divisiones",
                column: "DireccionInmuebleId",
                principalTable: "Direcciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Direcciones_Comunas_ComunaId",
                table: "Direcciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Direcciones_Provincias_ProvinciaId",
                table: "Direcciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Direcciones_Regiones_RegionId",
                table: "Direcciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Divisiones_Direcciones_DireccionInmuebleId",
                table: "Divisiones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Direcciones",
                table: "Direcciones");

            migrationBuilder.RenameTable(
                name: "Direcciones",
                newName: "Direccion");

            migrationBuilder.RenameIndex(
                name: "IX_Direcciones_RegionId",
                table: "Direccion",
                newName: "IX_Direccion_RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Direcciones_ProvinciaId",
                table: "Direccion",
                newName: "IX_Direccion_ProvinciaId");

            migrationBuilder.RenameIndex(
                name: "IX_Direcciones_ComunaId",
                table: "Direccion",
                newName: "IX_Direccion_ComunaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Direccion",
                table: "Direccion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Direccion_Comunas_ComunaId",
                table: "Direccion",
                column: "ComunaId",
                principalTable: "Comunas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Direccion_Provincias_ProvinciaId",
                table: "Direccion",
                column: "ProvinciaId",
                principalTable: "Provincias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Direccion_Regiones_RegionId",
                table: "Direccion",
                column: "RegionId",
                principalTable: "Regiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Divisiones_Direccion_DireccionInmuebleId",
                table: "Divisiones",
                column: "DireccionInmuebleId",
                principalTable: "Direccion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
