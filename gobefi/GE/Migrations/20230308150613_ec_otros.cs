using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class ec_otros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CMetroTc",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CTpeA",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CTpeM",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "DMetroTc",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "DTpeA",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "DTpeM",
                table: "EncuestaColaboradores",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CMetroTc",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "CTpeA",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "CTpeM",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "DMetroTc",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "DTpeA",
                table: "EncuestaColaboradores");

            migrationBuilder.DropColumn(
                name: "DTpeM",
                table: "EncuestaColaboradores");
        }
    }
}
