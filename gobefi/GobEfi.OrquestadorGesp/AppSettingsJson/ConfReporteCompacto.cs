using System.Collections.Generic;

namespace OrquestadorGesp.AppSettingsJson
{
	public class ConfReporteCompacto : ConfReporteConInsumoRepExt
	{
		public const string PATRON_FECHA_GENERACION_EXCEL_DISPLAY = "{dd-mm-yyyy}";
		public const string FRASE_GENERACION_FECHA_REGEX_PATTERN = "al ([0-9]{2}-[0-9]{2}-[0-9]{4})";
		public readonly string[] TIPO_MEDIDOR = new string[] {"Medidor Compartido", "Medidor Exclusivo"};
		public readonly string[] TIPO_EDIFICIO = new string[] { "Eléctrico", "Mixto" };
		public const string PLANTILLA_TOTAL_ANHO_TITULO = "Total {0}";
		public const string MOLDE_STRING_FORMULA_SUMA = "SUM(${0}{1}:${2}{1})";
		public const string PATRON_REGEX_FORMULA_SUMA = "\\$([A-Z]{1,3})\\d+:\\$([A-Z]{1,3})\\d+";
		public const int LARGO_PRIMERA_DIMENSION_ARR_ESTILOS = 2;
		public readonly HashSet<string> NOMBRES_COLS_EXCELS_NUMEROS = new HashSet<string>() { "IdDivision", "Superficie" };
		public string ColumnaInicialExcelRecuadroAnho { get; set; }
		public int AnhoInicioRecuadros { get; set; }
		public string NombreEnergeticoTodos { get; set; }
		public string CeldaNombreServicioExcel { get; set; }
		public string CeldaFraseFechaGeneracion { get; set; }
		public string[] ColoresPorAnho { get; set; }
	}
}