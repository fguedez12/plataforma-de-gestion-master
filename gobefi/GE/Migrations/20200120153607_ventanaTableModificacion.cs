using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class ventanaTableModificacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventanas_Pisos_PisoId",
                table: "Ventanas");

            migrationBuilder.RenameColumn(
                name: "Supercifie",
                table: "Ventanas",
                newName: "Superficie");

            migrationBuilder.RenameColumn(
                name: "PisoId",
                table: "Ventanas",
                newName: "MuroId");

            migrationBuilder.RenameIndex(
                name: "IX_Ventanas_PisoId",
                table: "Ventanas",
                newName: "IX_Ventanas_MuroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventanas_Muros_MuroId",
                table: "Ventanas",
                column: "MuroId",
                principalTable: "Muros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventanas_Muros_MuroId",
                table: "Ventanas");

            migrationBuilder.RenameColumn(
                name: "Superficie",
                table: "Ventanas",
                newName: "Supercifie");

            migrationBuilder.RenameColumn(
                name: "MuroId",
                table: "Ventanas",
                newName: "PisoId");

            migrationBuilder.RenameIndex(
                name: "IX_Ventanas_MuroId",
                table: "Ventanas",
                newName: "IX_Ventanas_PisoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventanas_Pisos_PisoId",
                table: "Ventanas",
                column: "PisoId",
                principalTable: "Pisos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
