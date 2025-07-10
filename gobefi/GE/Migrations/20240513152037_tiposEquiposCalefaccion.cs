using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class tiposEquiposCalefaccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TiposEquiposCalefaccion",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Rendimiento = table.Column<decimal>(nullable: false),
                    A = table.Column<decimal>(nullable: false),
                    B = table.Column<decimal>(nullable: false),
                    C = table.Column<decimal>(nullable: false),
                    Temp = table.Column<decimal>(nullable: false),
                    Costo = table.Column<decimal>(nullable: false),
                    Costo_Social = table.Column<decimal>(nullable: false),
                    Costo_Mant = table.Column<decimal>(nullable: false),
                    Costo_Social_Mant = table.Column<decimal>(nullable: false),
                    Ejec_HD_Maestro = table.Column<decimal>(nullable: false),
                    Ejec_HD_Ayte = table.Column<decimal>(nullable: false),
                    Ejec_HD_Jornal = table.Column<decimal>(nullable: false),
                    Mant_HD_Maestro = table.Column<decimal>(nullable: false),
                    Mant_HD_Ayte = table.Column<decimal>(nullable: false),
                    Mant_HD_Jornal = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposEquiposCalefaccion", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TiposEquiposCalefaccion");
        }
    }
}
