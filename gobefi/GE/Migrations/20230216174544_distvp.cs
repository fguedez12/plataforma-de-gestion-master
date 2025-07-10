using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class distvp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "D1DisP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D1ElP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D1GasP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D1HibP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D1HidP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D2DisP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D2ElP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D2GasP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D2HibP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D2HidP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D3DisP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D3ElP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D3GasP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D3HibP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D3HidP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4DisP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4ElP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4GasP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4HibP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4HidP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4mDisP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4mElP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4mGasP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4mHibP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4mHidP",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "D1DisP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D1ElP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D1GasP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D1HibP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D1HidP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D2DisP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D2ElP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D2GasP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D2HibP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D2HidP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D3DisP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D3ElP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D3GasP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D3HibP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D3HidP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4DisP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4ElP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4GasP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4HibP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4HidP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4mDisP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4mElP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4mGasP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4mHibP",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "D4mHidP",
                table: "EncuestaColaboradores");
        }
    }
}
