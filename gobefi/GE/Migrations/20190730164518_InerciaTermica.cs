using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class InerciaTermica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "InerciaTermicaId",
                table: "Edificios",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InerciaTermicas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Version = table.Column<long>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InerciaTermicas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Edificios_InerciaTermicaId",
                table: "Edificios",
                column: "InerciaTermicaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Edificios_InerciaTermicas_InerciaTermicaId",
                table: "Edificios",
                column: "InerciaTermicaId",
                principalTable: "InerciaTermicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Edificios_InerciaTermicas_InerciaTermicaId",
                table: "Edificios");

            migrationBuilder.DropTable(
                name: "InerciaTermicas");

            migrationBuilder.DropIndex(
                name: "IX_Edificios_InerciaTermicaId",
                table: "Edificios");

            migrationBuilder.DropColumn(
                name: "InerciaTermicaId",
                table: "Edificios");
        }
    }
}
