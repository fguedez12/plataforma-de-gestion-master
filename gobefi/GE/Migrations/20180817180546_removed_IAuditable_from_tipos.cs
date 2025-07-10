using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class removed_IAuditable_from_tipos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "TipoVentanas");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TipoVentanas");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TipoVentanas");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TipoVentanas");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TipoVentanas");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "TipoVentanas");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "TipoUnidades");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TipoUnidades");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TipoUnidades");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TipoUnidades");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TipoUnidades");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "TipoUnidades");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "TipoTecnologias");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TipoTecnologias");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TipoTecnologias");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TipoTecnologias");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TipoTecnologias");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "TipoTecnologias");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "TipoTechos");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TipoTechos");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TipoTechos");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TipoTechos");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TipoTechos");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "TipoTechos");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "TipoSombreados");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TipoSombreados");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TipoSombreados");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TipoSombreados");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TipoSombreados");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "TipoSombreados");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "TipoPuertas");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TipoPuertas");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TipoPuertas");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TipoPuertas");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TipoPuertas");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "TipoPuertas");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "TipoPropiedades");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TipoPropiedades");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TipoPropiedades");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TipoPropiedades");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TipoPropiedades");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "TipoPropiedades");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "TipoMateriales");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TipoMateriales");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TipoMateriales");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TipoMateriales");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TipoMateriales");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "TipoMateriales");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "TipoEdificios");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TipoEdificios");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TipoEdificios");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TipoEdificios");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TipoEdificios");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "TipoEdificios");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "TipoAislaciones");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TipoAislaciones");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TipoAislaciones");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TipoAislaciones");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TipoAislaciones");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "TipoAislaciones");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "TipoAgrupamientos");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TipoAgrupamientos");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TipoAgrupamientos");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TipoAgrupamientos");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TipoAgrupamientos");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "TipoAgrupamientos");

            migrationBuilder.CreateTable(
                name: "TipoUsoModel",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsoModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoUsoModel");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "TipoVentanas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TipoVentanas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TipoVentanas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TipoVentanas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TipoVentanas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "TipoVentanas",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "TipoUnidades",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TipoUnidades",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TipoUnidades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TipoUnidades",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TipoUnidades",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "TipoUnidades",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "TipoTecnologias",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TipoTecnologias",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TipoTecnologias",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TipoTecnologias",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TipoTecnologias",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "TipoTecnologias",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "TipoTechos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TipoTechos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TipoTechos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TipoTechos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TipoTechos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "TipoTechos",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "TipoSombreados",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TipoSombreados",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TipoSombreados",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TipoSombreados",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TipoSombreados",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "TipoSombreados",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "TipoPuertas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TipoPuertas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TipoPuertas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TipoPuertas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TipoPuertas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "TipoPuertas",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "TipoPropiedades",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TipoPropiedades",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TipoPropiedades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TipoPropiedades",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TipoPropiedades",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "TipoPropiedades",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "TipoMateriales",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TipoMateriales",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TipoMateriales",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TipoMateriales",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TipoMateriales",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "TipoMateriales",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "TipoEdificios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TipoEdificios",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TipoEdificios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TipoEdificios",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TipoEdificios",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "TipoEdificios",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "TipoAislaciones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TipoAislaciones",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TipoAislaciones",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TipoAislaciones",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TipoAislaciones",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "TipoAislaciones",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "TipoAgrupamientos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TipoAgrupamientos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TipoAgrupamientos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TipoAgrupamientos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TipoAgrupamientos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "TipoAgrupamientos",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
