using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class encuestaColab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EncuestaColaboradores",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    C1HibP = table.Column<int>(nullable: false),
                    C2HibP = table.Column<int>(nullable: false),
                    C3HibP = table.Column<int>(nullable: false),
                    C4HibP = table.Column<int>(nullable: false),
                    C4mHibP = table.Column<int>(nullable: false),
                    C1HidP = table.Column<int>(nullable: false),
                    C2HidP = table.Column<int>(nullable: false),
                    C3HidP = table.Column<int>(nullable: false),
                    C4HidP = table.Column<int>(nullable: false),
                    C4mHidP = table.Column<int>(nullable: false),
                    C1ElP = table.Column<int>(nullable: false),
                    C2ElP = table.Column<int>(nullable: false),
                    C3ElP = table.Column<int>(nullable: false),
                    C4ElP = table.Column<int>(nullable: false),
                    C4mElP = table.Column<int>(nullable: false),
                    C1DisP = table.Column<int>(nullable: false),
                    C2DisP = table.Column<int>(nullable: false),
                    C3DisP = table.Column<int>(nullable: false),
                    C4DisP = table.Column<int>(nullable: false),
                    C4mDisP = table.Column<int>(nullable: false),
                    C1GasP = table.Column<int>(nullable: false),
                    C2GasP = table.Column<int>(nullable: false),
                    C3GasP = table.Column<int>(nullable: false),
                    C4GasP = table.Column<int>(nullable: false),
                    C4mGasP = table.Column<int>(nullable: false),
                    C1HibC = table.Column<int>(nullable: false),
                    C2HibC = table.Column<int>(nullable: false),
                    C3HibC = table.Column<int>(nullable: false),
                    C4HibC = table.Column<int>(nullable: false),
                    C4mHibC = table.Column<int>(nullable: false),
                    C1HidC = table.Column<int>(nullable: false),
                    C2HidC = table.Column<int>(nullable: false),
                    C3HidC = table.Column<int>(nullable: false),
                    C4HidC = table.Column<int>(nullable: false),
                    C4mHidC = table.Column<int>(nullable: false),
                    C1ElC = table.Column<int>(nullable: false),
                    C2ElC = table.Column<int>(nullable: false),
                    C3ElC = table.Column<int>(nullable: false),
                    C4ElC = table.Column<int>(nullable: false),
                    C4mElC = table.Column<int>(nullable: false),
                    C1DisC = table.Column<int>(nullable: false),
                    C2DisC = table.Column<int>(nullable: false),
                    C3DisC = table.Column<int>(nullable: false),
                    C4DisC = table.Column<int>(nullable: false),
                    C4mDisC = table.Column<int>(nullable: false),
                    C1GasC = table.Column<int>(nullable: false),
                    C2GasC = table.Column<int>(nullable: false),
                    C3GasC = table.Column<int>(nullable: false),
                    C4GasC = table.Column<int>(nullable: false),
                    C4mGasC = table.Column<int>(nullable: false),
                    CbusIu = table.Column<int>(nullable: false),
                    DbusIu = table.Column<int>(nullable: false),
                    CbusL = table.Column<int>(nullable: false),
                    DbusL = table.Column<int>(nullable: false),
                    CbusTs = table.Column<int>(nullable: false),
                    DbusTs = table.Column<int>(nullable: false),
                    CMetro = table.Column<int>(nullable: false),
                    DMetro = table.Column<int>(nullable: false),
                    CMetroT = table.Column<int>(nullable: false),
                    DMetroT = table.Column<int>(nullable: false),
                    CBiciTa = table.Column<int>(nullable: false),
                    DBiciTa = table.Column<int>(nullable: false),
                    CBiciEs = table.Column<int>(nullable: false),
                    DBiciEs = table.Column<int>(nullable: false),
                    CBiciAd = table.Column<int>(nullable: false),
                    DBiciAd = table.Column<int>(nullable: false),
                    CMotoEl = table.Column<int>(nullable: false),
                    DMotoEl = table.Column<int>(nullable: false),
                    CMotoGas = table.Column<int>(nullable: false),
                    DMotoGas = table.Column<int>(nullable: false),
                    CTaxi = table.Column<int>(nullable: false),
                    DTaxi = table.Column<int>(nullable: false),
                    CColectivo = table.Column<int>(nullable: false),
                    DColectivo = table.Column<int>(nullable: false),
                    CCaminando = table.Column<int>(nullable: false),
                    DCaminando = table.Column<int>(nullable: false),
                    CScooterEl = table.Column<int>(nullable: false),
                    DScooterEl = table.Column<int>(nullable: false),
                    CBiciEl = table.Column<int>(nullable: false),
                    DBiciEl = table.Column<int>(nullable: false),
                    ServicioId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncuestaColaboradores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EncuestaColaboradores_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaColaboradores_ServicioId",
                table: "EncuestaColaboradores",
                column: "ServicioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncuestaColaboradores");
        }
    }
}
