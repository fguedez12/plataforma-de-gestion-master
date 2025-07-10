using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.FV.API.Migrations
{
    public partial class modelos_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEm = table.Column<long>(type: "bigint", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Traccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transmision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Combustible = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Propulsion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cilindrada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Carroceria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Codigo_informe_tecnico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha_homologacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Categoria_vehiculo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Empresa_homologacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Norma_emisiones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Co2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rendimiento_ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rendimiento_carretera = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rendimiento_mixto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rendimiento_puro_electrico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rendimiento_enchufable_combustible = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rendimiento_enchufable_electrico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo_de_conector_ac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo_de_conector_dc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Acumulacion_energia_bateria = table.Column<int>(type: "int", nullable: false),
                    Capacidad_convertidor_vehiculo_electrico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Autonomia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rendimiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eliminar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Modelos");
        }
    }
}
