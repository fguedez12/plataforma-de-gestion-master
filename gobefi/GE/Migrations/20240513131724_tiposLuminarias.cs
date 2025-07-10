using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class tiposLuminarias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TiposLuminarias",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Q_Educacion = table.Column<decimal>(nullable: false),
                    Q_Oficinas = table.Column<decimal>(nullable: false),
                    Q_Salud = table.Column<decimal>(nullable: false),
                    Q_Seguridad = table.Column<decimal>(nullable: false),
                    Area_Educacion = table.Column<decimal>(nullable: false),
                    Area_Oficinas = table.Column<decimal>(nullable: false),
                    Area_Salud = table.Column<decimal>(nullable: false),
                    Area_Seguridad = table.Column<decimal>(nullable: false),
                    Vida_Util = table.Column<int>(nullable: false),
                    Costo_Lamp = table.Column<int>(nullable: false),
                    Costo_Lum = table.Column<int>(nullable: false),
                    Costo_Social_Lamp = table.Column<int>(nullable: false),
                    Costo_Social_Lum = table.Column<int>(nullable: false),
                    Ejec_HD_Maestro = table.Column<decimal>(nullable: false),
                    Ejec_HD_Ayte = table.Column<decimal>(nullable: false),
                    Ejec_HD_Jornal = table.Column<decimal>(nullable: false),
                    Rep_HD_Maestro = table.Column<decimal>(nullable: false),
                    Rep_HD_Ayte = table.Column<decimal>(nullable: false),
                    Rep_HD_Jornal = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposLuminarias", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TiposLuminarias");
        }
    }
}
