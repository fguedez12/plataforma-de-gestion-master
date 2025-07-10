using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class RelacionCompraMedidores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivoAdjuntos_Compras_CompraId",
                table: "ArchivoAdjuntos");

            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Medidores_MedidorId",
                table: "Compras");

            migrationBuilder.DropForeignKey(
                name: "FK_EnergeticoUnidadesMedidas_UnidadesMedidas_UnidadMedidaId",
                table: "EnergeticoUnidadesMedidas");

            migrationBuilder.DropTable(
                name: "EnergeticoDivisionNClientes");

            migrationBuilder.DropIndex(
                name: "IX_Compras_MedidorId",
                table: "Compras");

            migrationBuilder.DropIndex(
                name: "IX_ArchivoAdjuntos_CompraId",
                table: "ArchivoAdjuntos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnidadesMedidas",
                table: "UnidadesMedidas");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "MedidorId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "Monto",
                table: "Compras");

            migrationBuilder.RenameTable(
                name: "UnidadesMedidas",
                newName: "UnidadesMedida");

            migrationBuilder.AddColumn<long>(
                name: "UnidadesMedidaId",
                table: "Medidores",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "FacturaId",
                table: "Compras",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "CompraId",
                table: "ArchivoAdjuntos",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnidadesMedida",
                table: "UnidadesMedida",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CompraMedidor",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Consumo = table.Column<double>(nullable: false),
                    MedidorId = table.Column<long>(nullable: false),
                    CompraId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraMedidor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompraMedidor_Compras_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraMedidor_Medidores_MedidorId",
                        column: x => x.MedidorId,
                        principalTable: "Medidores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnergeticoDivision",
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
                    DivisionId = table.Column<long>(nullable: false),
                    EnergeticoId = table.Column<long>(nullable: false),
                    NumeroClienteId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergeticoDivision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergeticoDivision_Divisiones_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Divisiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnergeticoDivision_Energeticos_EnergeticoId",
                        column: x => x.EnergeticoId,
                        principalTable: "Energeticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnergeticoDivision_NumeroClientes_NumeroClienteId",
                        column: x => x.NumeroClienteId,
                        principalTable: "NumeroClientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medidores_UnidadesMedidaId",
                table: "Medidores",
                column: "UnidadesMedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivoAdjuntos_CompraId",
                table: "ArchivoAdjuntos",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraMedidor_CompraId",
                table: "CompraMedidor",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraMedidor_MedidorId",
                table: "CompraMedidor",
                column: "MedidorId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergeticoDivision_DivisionId",
                table: "EnergeticoDivision",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergeticoDivision_EnergeticoId",
                table: "EnergeticoDivision",
                column: "EnergeticoId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergeticoDivision_NumeroClienteId",
                table: "EnergeticoDivision",
                column: "NumeroClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivoAdjuntos_Compras_CompraId",
                table: "ArchivoAdjuntos",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EnergeticoUnidadesMedidas_UnidadesMedida_UnidadMedidaId",
                table: "EnergeticoUnidadesMedidas",
                column: "UnidadMedidaId",
                principalTable: "UnidadesMedida",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medidores_UnidadesMedida_UnidadesMedidaId",
                table: "Medidores",
                column: "UnidadesMedidaId",
                principalTable: "UnidadesMedida",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivoAdjuntos_Compras_CompraId",
                table: "ArchivoAdjuntos");

            migrationBuilder.DropForeignKey(
                name: "FK_EnergeticoUnidadesMedidas_UnidadesMedida_UnidadMedidaId",
                table: "EnergeticoUnidadesMedidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Medidores_UnidadesMedida_UnidadesMedidaId",
                table: "Medidores");

            migrationBuilder.DropTable(
                name: "CompraMedidor");

            migrationBuilder.DropTable(
                name: "EnergeticoDivision");

            migrationBuilder.DropIndex(
                name: "IX_Medidores_UnidadesMedidaId",
                table: "Medidores");

            migrationBuilder.DropIndex(
                name: "IX_ArchivoAdjuntos_CompraId",
                table: "ArchivoAdjuntos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnidadesMedida",
                table: "UnidadesMedida");

            migrationBuilder.DropColumn(
                name: "UnidadesMedidaId",
                table: "Medidores");

            migrationBuilder.DropColumn(
                name: "FacturaId",
                table: "Compras");

            migrationBuilder.RenameTable(
                name: "UnidadesMedida",
                newName: "UnidadesMedidas");

            migrationBuilder.AddColumn<double>(
                name: "Cantidad",
                table: "Compras",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "MedidorId",
                table: "Compras",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Monto",
                table: "Compras",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<long>(
                name: "CompraId",
                table: "ArchivoAdjuntos",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnidadesMedidas",
                table: "UnidadesMedidas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EnergeticoDivisionNClientes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DivisionId = table.Column<long>(nullable: false),
                    EnergeticoId = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    NumeroClienteId = table.Column<long>(nullable: true),
                    UnidadMedidaId = table.Column<long>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Version = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergeticoDivisionNClientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergeticoDivisionNClientes_Divisiones_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Divisiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnergeticoDivisionNClientes_Energeticos_EnergeticoId",
                        column: x => x.EnergeticoId,
                        principalTable: "Energeticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnergeticoDivisionNClientes_UnidadesMedidas_UnidadMedidaId",
                        column: x => x.UnidadMedidaId,
                        principalTable: "UnidadesMedidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_MedidorId",
                table: "Compras",
                column: "MedidorId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivoAdjuntos_CompraId",
                table: "ArchivoAdjuntos",
                column: "CompraId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnergeticoDivisionNClientes_DivisionId",
                table: "EnergeticoDivisionNClientes",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergeticoDivisionNClientes_EnergeticoId",
                table: "EnergeticoDivisionNClientes",
                column: "EnergeticoId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergeticoDivisionNClientes_UnidadMedidaId",
                table: "EnergeticoDivisionNClientes",
                column: "UnidadMedidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivoAdjuntos_Compras_CompraId",
                table: "ArchivoAdjuntos",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Medidores_MedidorId",
                table: "Compras",
                column: "MedidorId",
                principalTable: "Medidores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EnergeticoUnidadesMedidas_UnidadesMedidas_UnidadMedidaId",
                table: "EnergeticoUnidadesMedidas",
                column: "UnidadMedidaId",
                principalTable: "UnidadesMedidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
