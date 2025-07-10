using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class removeTableEnergeticoCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnergeticoCliente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnergeticoCliente",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    EnergeticoId = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    NumClienteId = table.Column<long>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Version = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergeticoCliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergeticoCliente_Energeticos_EnergeticoId",
                        column: x => x.EnergeticoId,
                        principalTable: "Energeticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnergeticoCliente_NumeroClientes_NumClienteId",
                        column: x => x.NumClienteId,
                        principalTable: "NumeroClientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnergeticoCliente_EnergeticoId",
                table: "EnergeticoCliente",
                column: "EnergeticoId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergeticoCliente_NumClienteId",
                table: "EnergeticoCliente",
                column: "NumClienteId");
        }
    }
}
