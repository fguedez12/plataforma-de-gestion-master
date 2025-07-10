using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class ChangeNameUnidadToUnidadMedidaAndAddFKEnergeticoDivisionNCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnergeticoUnidades");

            migrationBuilder.DropTable(
                name: "Unidades");

            migrationBuilder.AddColumn<long>(
                name: "EnergeticoUnMedidaId",
                table: "EnergeticoDivisionNClientes",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "UnidadesMedidas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Abrv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesMedidas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnergeticoUnidadesMedidas",
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
                    Calor = table.Column<double>(nullable: false),
                    Densidad = table.Column<double>(nullable: false),
                    Factor = table.Column<double>(nullable: false),
                    EnergeticoId = table.Column<long>(nullable: false),
                    UnidadMedidaId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergeticoUnidadesMedidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergeticoUnidadesMedidas_Energeticos_EnergeticoId",
                        column: x => x.EnergeticoId,
                        principalTable: "Energeticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_EnergeticoUnidadesMedidas_UnidadesMedidas_UnidadMedidaId",
                        column: x => x.UnidadMedidaId,
                        principalTable: "UnidadesMedidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnergeticoDivisionNClientes_EnergeticoUnMedidaId",
                table: "EnergeticoDivisionNClientes",
                column: "EnergeticoUnMedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergeticoUnidadesMedidas_EnergeticoId",
                table: "EnergeticoUnidadesMedidas",
                column: "EnergeticoId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergeticoUnidadesMedidas_UnidadMedidaId",
                table: "EnergeticoUnidadesMedidas",
                column: "UnidadMedidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnergeticoDivisionNClientes_EnergeticoUnidadesMedidas_EnergeticoUnMedidaId",
                table: "EnergeticoDivisionNClientes",
                column: "EnergeticoUnMedidaId",
                principalTable: "EnergeticoUnidadesMedidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnergeticoDivisionNClientes_EnergeticoUnidadesMedidas_EnergeticoUnMedidaId",
                table: "EnergeticoDivisionNClientes");

            migrationBuilder.DropTable(
                name: "EnergeticoUnidadesMedidas");

            migrationBuilder.DropTable(
                name: "UnidadesMedidas");

            migrationBuilder.DropIndex(
                name: "IX_EnergeticoDivisionNClientes_EnergeticoUnMedidaId",
                table: "EnergeticoDivisionNClientes");

            migrationBuilder.DropColumn(
                name: "EnergeticoUnMedidaId",
                table: "EnergeticoDivisionNClientes");

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Abrv = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnergeticoUnidades",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Calor = table.Column<double>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Densidad = table.Column<double>(nullable: false),
                    EnergeticoId = table.Column<long>(nullable: false),
                    Factor = table.Column<double>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    UnidadId = table.Column<long>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Version = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergeticoUnidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergeticoUnidades_Energeticos_EnergeticoId",
                        column: x => x.EnergeticoId,
                        principalTable: "Energeticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnergeticoUnidades_Unidades_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnergeticoUnidades_EnergeticoId",
                table: "EnergeticoUnidades",
                column: "EnergeticoId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergeticoUnidades_UnidadId",
                table: "EnergeticoUnidades",
                column: "UnidadId");
        }
    }
}
