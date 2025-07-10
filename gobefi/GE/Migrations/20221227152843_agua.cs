using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class agua : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aguas",
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
                    TipoSuministroId = table.Column<int>(nullable: false),
                    CompraAgredada = table.Column<bool>(nullable: false),
                    InicioLectura = table.Column<DateTime>(nullable: true),
                    FinLectura = table.Column<DateTime>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: true),
                    AnioAdquisicion = table.Column<int>(nullable: false),
                    Cantidad = table.Column<double>(nullable: false),
                    Costo = table.Column<int>(nullable: true),
                    AdjuntoUrl = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aguas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aguas");
        }
    }
}
