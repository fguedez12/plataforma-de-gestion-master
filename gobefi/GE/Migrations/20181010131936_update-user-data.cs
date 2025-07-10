using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class updateuserdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsAuditor",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsGestorConsulta",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsGestorServicio",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsGestorUnidad",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsUser",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAuditor",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsGestorConsulta",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsGestorServicio",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsGestorUnidad",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUser",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }
    }
}
