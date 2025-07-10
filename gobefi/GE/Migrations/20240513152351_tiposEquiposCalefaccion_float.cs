using Microsoft.EntityFrameworkCore.Migrations;

namespace GobEfi.Web.Migrations
{
    public partial class tiposEquiposCalefaccion_float : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Rep_HD_Maestro",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Rep_HD_Jornal",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Rep_HD_Ayte",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Q_Seguridad",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Q_Salud",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Q_Oficinas",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Q_Educacion",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Ejec_HD_Maestro",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Ejec_HD_Jornal",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Ejec_HD_Ayte",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Area_Seguridad",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Area_Salud",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Area_Oficinas",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Area_Educacion",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Temp",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Rendimiento",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Mant_HD_Maestro",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Mant_HD_Jornal",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Mant_HD_Ayte",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Ejec_HD_Maestro",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Ejec_HD_Jornal",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Ejec_HD_Ayte",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Costo_Social_Mant",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Costo_Social",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Costo_Mant",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Costo",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "C",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "B",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "A",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rep_HD_Maestro",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Rep_HD_Jornal",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Rep_HD_Ayte",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Q_Seguridad",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Q_Salud",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Q_Oficinas",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Q_Educacion",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Ejec_HD_Maestro",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Ejec_HD_Jornal",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Ejec_HD_Ayte",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Area_Seguridad",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Area_Salud",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Area_Oficinas",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Area_Educacion",
                table: "TiposLuminarias",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Temp",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Rendimiento",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Mant_HD_Maestro",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Mant_HD_Jornal",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Mant_HD_Ayte",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Ejec_HD_Maestro",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Ejec_HD_Jornal",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Ejec_HD_Ayte",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Costo_Social_Mant",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Costo_Social",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Costo_Mant",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Costo",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "C",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "B",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "A",
                table: "TiposEquiposCalefaccion",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
