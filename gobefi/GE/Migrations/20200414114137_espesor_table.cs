using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class espesor_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EspesorId",
                table: "Techos",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EspesorId",
                table: "Suelos",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EspesorId",
                table: "Muros",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Espesores",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Espesores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Techos_EspesorId",
                table: "Techos",
                column: "EspesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Suelos_EspesorId",
                table: "Suelos",
                column: "EspesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Muros_EspesorId",
                table: "Muros",
                column: "EspesorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Muros_Espesores_EspesorId",
                table: "Muros",
                column: "EspesorId",
                principalTable: "Espesores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suelos_Espesores_EspesorId",
                table: "Suelos",
                column: "EspesorId",
                principalTable: "Espesores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Techos_Espesores_EspesorId",
                table: "Techos",
                column: "EspesorId",
                principalTable: "Espesores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Muros_Espesores_EspesorId",
                table: "Muros");

            migrationBuilder.DropForeignKey(
                name: "FK_Suelos_Espesores_EspesorId",
                table: "Suelos");

            migrationBuilder.DropForeignKey(
                name: "FK_Techos_Espesores_EspesorId",
                table: "Techos");

            migrationBuilder.DropTable(
                name: "Espesores");

            migrationBuilder.DropIndex(
                name: "IX_Techos_EspesorId",
                table: "Techos");

            migrationBuilder.DropIndex(
                name: "IX_Suelos_EspesorId",
                table: "Suelos");

            migrationBuilder.DropIndex(
                name: "IX_Muros_EspesorId",
                table: "Muros");

            migrationBuilder.DropColumn(
                name: "EspesorId",
                table: "Techos");

            migrationBuilder.DropColumn(
                name: "EspesorId",
                table: "Suelos");

            migrationBuilder.DropColumn(
                name: "EspesorId",
                table: "Muros");
        }
    }
}
