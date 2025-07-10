using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class porcentajempfloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "PorcentajeConcientizadosEtapa2",
                table: "Documentos",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "PorcentajeCapacitadosEtapa2",
                table: "Documentos",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PorcentajeConcientizadosEtapa2",
                table: "Documentos",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PorcentajeCapacitadosEtapa2",
                table: "Documentos",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);
        }
    }
}
