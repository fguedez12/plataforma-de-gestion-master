using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class add_permiteMedidor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PermiteMedidor",
                table: "Energeticos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermiteMedidor",
                table: "Energeticos");
        }
    }
}
