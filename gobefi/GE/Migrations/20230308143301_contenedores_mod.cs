using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class contenedores_mod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Contenedores");

            migrationBuilder.DropColumn(
                name: "UbicacionId",
                table: "Contenedores");

            migrationBuilder.RenameColumn(
                name: "Direccion",
                table: "Contenedores",
                newName: "Ubicacion");

            migrationBuilder.AlterColumn<int>(
                name: "NRecipientes",
                table: "Contenedores",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Capacidad",
                table: "Contenedores",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Propiedad",
                table: "Contenedores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Propiedad",
                table: "Contenedores");

            migrationBuilder.RenameColumn(
                name: "Ubicacion",
                table: "Contenedores",
                newName: "Direccion");

            migrationBuilder.AlterColumn<int>(
                name: "NRecipientes",
                table: "Contenedores",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Capacidad",
                table: "Contenedores",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "Contenedores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UbicacionId",
                table: "Contenedores",
                nullable: false,
                defaultValue: 0);
        }
    }
}
