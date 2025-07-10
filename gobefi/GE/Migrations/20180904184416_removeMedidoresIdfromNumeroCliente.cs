using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class removeMedidoresIdfromNumeroCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Medidores_Divisiones_DivisionId",
            //    table: "Medidores");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Medidores_EnergeticoDivisiones_EnergeticoDivisionId",
            //    table: "Medidores");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_NumeroClientes_Divisiones_DivisionId",
            //    table: "NumeroClientes");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_NumeroClientes_EnergeticoDivisiones_EnergeticoDivisionId",
            //    table: "NumeroClientes");

            //migrationBuilder.DropTable(
            //    name: "EnergeticoDivisiones");

            //migrationBuilder.DropIndex(
            //    name: "IX_NumeroClientes_EnergeticoDivisionId",
            //    table: "NumeroClientes");

            //migrationBuilder.DropIndex(
            //    name: "IX_Medidores_EnergeticoDivisionId",
            //    table: "Medidores");

            //migrationBuilder.DropColumn(
            //    name: "EnergeticoDivisionId",
            //    table: "NumeroClientes");

            migrationBuilder.DropColumn(
                name: "MedidoresId",
                table: "NumeroClientes");

            //migrationBuilder.DropColumn(
            //    name: "EnergeticoDivisionId",
            //    table: "Medidores");

            migrationBuilder.AlterColumn<long>(
                name: "DivisionId",
                table: "NumeroClientes",
                nullable: true,
                oldClrType: typeof(long));

            //migrationBuilder.AlterColumn<long>(
            //    name: "DivisionId",
            //    table: "Medidores",
            //    nullable: true,
            //    oldClrType: typeof(long));

            //migrationBuilder.CreateTable(
            //    name: "EnergeticoDivisionNClientes",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        CreatedAt = table.Column<DateTime>(nullable: false),
            //        UpdatedAt = table.Column<DateTime>(nullable: false),
            //        Version = table.Column<long>(nullable: false),
            //        Active = table.Column<bool>(nullable: false),
            //        ModifiedBy = table.Column<string>(nullable: true),
            //        CreatedBy = table.Column<string>(nullable: true),
            //        DivisionId = table.Column<long>(nullable: false),
            //        EnergeticoId = table.Column<long>(nullable: false),
            //        NumeroClienteId = table.Column<long>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EnergeticoDivisionNClientes", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_EnergeticoDivisionNClientes_Divisiones_DivisionId",
            //            column: x => x.DivisionId,
            //            principalTable: "Divisiones",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_EnergeticoDivisionNClientes_Energeticos_EnergeticoId",
            //            column: x => x.EnergeticoId,
            //            principalTable: "Energeticos",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_EnergeticoDivisionNClientes_DivisionId",
            //    table: "EnergeticoDivisionNClientes",
            //    column: "DivisionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EnergeticoDivisionNClientes_EnergeticoId",
            //    table: "EnergeticoDivisionNClientes",
            //    column: "EnergeticoId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Medidores_Divisiones_DivisionId",
            //    table: "Medidores",
            //    column: "DivisionId",
            //    principalTable: "Divisiones",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_NumeroClientes_Divisiones_DivisionId",
            //    table: "NumeroClientes",
            //    column: "DivisionId",
            //    principalTable: "Divisiones",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medidores_Divisiones_DivisionId",
                table: "Medidores");

            migrationBuilder.DropForeignKey(
                name: "FK_NumeroClientes_Divisiones_DivisionId",
                table: "NumeroClientes");

            migrationBuilder.DropTable(
                name: "EnergeticoDivisionNClientes");

            migrationBuilder.AlterColumn<long>(
                name: "DivisionId",
                table: "NumeroClientes",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EnergeticoDivisionId",
                table: "NumeroClientes",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MedidoresId",
                table: "NumeroClientes",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "DivisionId",
                table: "Medidores",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EnergeticoDivisionId",
                table: "Medidores",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "EnergeticoDivisiones",
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
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Version = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergeticoDivisiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergeticoDivisiones_Divisiones_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Divisiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnergeticoDivisiones_Energeticos_EnergeticoId",
                        column: x => x.EnergeticoId,
                        principalTable: "Energeticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NumeroClientes_EnergeticoDivisionId",
                table: "NumeroClientes",
                column: "EnergeticoDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Medidores_EnergeticoDivisionId",
                table: "Medidores",
                column: "EnergeticoDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergeticoDivisiones_DivisionId",
                table: "EnergeticoDivisiones",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergeticoDivisiones_EnergeticoId",
                table: "EnergeticoDivisiones",
                column: "EnergeticoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medidores_Divisiones_DivisionId",
                table: "Medidores",
                column: "DivisionId",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medidores_EnergeticoDivisiones_EnergeticoDivisionId",
                table: "Medidores",
                column: "EnergeticoDivisionId",
                principalTable: "EnergeticoDivisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NumeroClientes_Divisiones_DivisionId",
                table: "NumeroClientes",
                column: "DivisionId",
                principalTable: "Divisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NumeroClientes_EnergeticoDivisiones_EnergeticoDivisionId",
                table: "NumeroClientes",
                column: "EnergeticoDivisionId",
                principalTable: "EnergeticoDivisiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
