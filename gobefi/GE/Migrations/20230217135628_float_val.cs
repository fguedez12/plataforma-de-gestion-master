using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class float_val : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "DTaxi",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "DScooterEl",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "DMotoGas",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "DMotoEl",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "DMetroT",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "DMetro",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "DColectivo",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "DCaminando",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "DBusTs",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "DBusL",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "DBusIu",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "DBiciTa",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "DBiciEs",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "DBiciEl",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "DBiciAd",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DTaxi",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "DScooterEl",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "DMotoGas",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "DMotoEl",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "DMetroT",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "DMetro",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "DColectivo",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "DCaminando",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "DBusTs",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "DBusL",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "DBusIu",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "DBiciTa",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "DBiciEs",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "DBiciEl",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "DBiciAd",
                table: "EncuestaColaboradores",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
