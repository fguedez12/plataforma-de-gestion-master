using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class permisosback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EndPoints",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ControllerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndPoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermisosBackEnd",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndPointId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    Escritura = table.Column<bool>(nullable: false),
                    Lectura = table.Column<bool>(nullable: false),
                    RolId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermisosBackEnd", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermisosBackEnd_EndPoints_EndPointId",
                        column: x => x.EndPointId,
                        principalTable: "EndPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermisosBackEnd_AspNetRoles_RolId",
                        column: x => x.RolId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermisosBackEnd_EndPointId",
                table: "PermisosBackEnd",
                column: "EndPointId");

            migrationBuilder.CreateIndex(
                name: "IX_PermisosBackEnd_RolId",
                table: "PermisosBackEnd",
                column: "RolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermisosBackEnd");

            migrationBuilder.DropTable(
                name: "EndPoints");
        }
    }
}
