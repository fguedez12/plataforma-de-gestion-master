using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class tipo_uso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TipoUsoId",
                table: "Divisiones",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TipoUsos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Divisiones_TipoUsoId",
                table: "Divisiones",
                column: "TipoUsoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Divisiones_TipoUsos_TipoUsoId",
                table: "Divisiones",
                column: "TipoUsoId",
                principalTable: "TipoUsos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Divisiones_TipoUsos_TipoUsoId",
                table: "Divisiones");

            migrationBuilder.DropTable(
                name: "TipoUsos");

            migrationBuilder.DropIndex(
                name: "IX_Divisiones_TipoUsoId",
                table: "Divisiones");

            migrationBuilder.DropColumn(
                name: "TipoUsoId",
                table: "Divisiones");
        }
    }
}
