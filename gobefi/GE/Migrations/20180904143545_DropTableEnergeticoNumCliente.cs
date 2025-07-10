using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class DropTableEnergeticoNumCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnergeticoNumClientes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnergeticoNumClientes",
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
                    table.PrimaryKey("PK_EnergeticoNumClientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergeticoNumClientes_Energeticos_EnergeticoId",
                        column: x => x.EnergeticoId,
                        principalTable: "Energeticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnergeticoNumClientes_NumeroClientes_NumClienteId",
                        column: x => x.NumClienteId,
                        principalTable: "NumeroClientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnergeticoNumClientes_EnergeticoId",
                table: "EnergeticoNumClientes",
                column: "EnergeticoId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergeticoNumClientes_NumClienteId",
                table: "EnergeticoNumClientes",
                column: "NumClienteId");
        }
    }
}
