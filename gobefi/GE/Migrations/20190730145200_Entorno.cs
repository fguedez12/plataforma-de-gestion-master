using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class Entorno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EntornoId",
                table: "Edificios",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Entornos",
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
                    table.PrimaryKey("PK_Entornos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Edificios_EntornoId",
                table: "Edificios",
                column: "EntornoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Edificios_Entornos_EntornoId",
                table: "Edificios",
                column: "EntornoId",
                principalTable: "Entornos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Edificios_Entornos_EntornoId",
                table: "Edificios");

            migrationBuilder.DropTable(
                name: "Entornos");

            migrationBuilder.DropIndex(
                name: "IX_Edificios_EntornoId",
                table: "Edificios");

            migrationBuilder.DropColumn(
                name: "EntornoId",
                table: "Edificios");
        }
    }
}
