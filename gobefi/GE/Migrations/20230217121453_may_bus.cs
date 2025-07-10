using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class may_bus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DbusTs",
                table: "EncuestaColaboradores",
                newName: "DBusTs");

            migrationBuilder.RenameColumn(
                name: "DbusL",
                table: "EncuestaColaboradores",
                newName: "DBusL");

            migrationBuilder.RenameColumn(
                name: "DbusIu",
                table: "EncuestaColaboradores",
                newName: "DBusIu");

            migrationBuilder.RenameColumn(
                name: "CbusTs",
                table: "EncuestaColaboradores",
                newName: "CBusTs");

            migrationBuilder.RenameColumn(
                name: "CbusL",
                table: "EncuestaColaboradores",
                newName: "CBusL");

            migrationBuilder.RenameColumn(
                name: "CbusIu",
                table: "EncuestaColaboradores",
                newName: "CBusIu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DBusTs",
                table: "EncuestaColaboradores",
                newName: "DbusTs");

            migrationBuilder.RenameColumn(
                name: "DBusL",
                table: "EncuestaColaboradores",
                newName: "DbusL");

            migrationBuilder.RenameColumn(
                name: "DBusIu",
                table: "EncuestaColaboradores",
                newName: "DbusIu");

            migrationBuilder.RenameColumn(
                name: "CBusTs",
                table: "EncuestaColaboradores",
                newName: "CbusTs");

            migrationBuilder.RenameColumn(
                name: "CBusL",
                table: "EncuestaColaboradores",
                newName: "CbusL");

            migrationBuilder.RenameColumn(
                name: "CBusIu",
                table: "EncuestaColaboradores",
                newName: "CbusIu");
        }
    }
}
