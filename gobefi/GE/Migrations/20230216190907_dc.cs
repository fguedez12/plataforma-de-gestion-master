using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class dc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "D1DisC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D1ElC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D1GasC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D1HibC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D1HidC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D2DisC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D2ElC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D2GasC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D2HibC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D2HidC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D3DisC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D3ElC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D3GasC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D3HibC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D3HidC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4DisC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4ElC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4GasC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4HibC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4HidC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4mDisC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4mElC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4mGasC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4mHibC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4mHidC",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "D1DisC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D1ElC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D1GasC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D1HibC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D1HidC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D2DisC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D2ElC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D2GasC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D2HibC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D2HidC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D3DisC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D3ElC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D3GasC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D3HibC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D3HidC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4DisC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4ElC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4GasC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4HibC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4HidC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4mDisC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4mElC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4mGasC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4mHibC",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4mHidC",
                table: "EncuestaColaboradores");
        }
    }
}
