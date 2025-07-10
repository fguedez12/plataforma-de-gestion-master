using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.AppSettingsJson
{
	public class ConfReporteBase
	{
		public const string VALOR_CAMPO_SIN_INFO = "Sin Informacion";
		public const string FORMATO_SOLO_FECHA = "dd/MM/yyyy";
		public const string FORMATO_SOLO_FECHA_CON_GUION = "dd-MM-yyyy";
		public const string FORMATO_SOLO_FECHA_MINUCULA = "dd/mm/yyyy";
		public const string FORMATO_SOLO_FECHA_MINUCULA_CON_GUION = "dd-mm-yyyy";
		public const string FORMATO_HORA_MIN_SEG = "HH:mm:ss";
    public const string FORMATO_HORA_MIN_SEG_FILESYSTEM = "HH'h'_mm'm'_ss's'";
    public const string FORMATO_FECHA_HORA = FORMATO_SOLO_FECHA + " " + FORMATO_HORA_MIN_SEG;
		public const string FORMATO_FECHA_HORA_CON_GUION = FORMATO_SOLO_FECHA_CON_GUION + " " + FORMATO_HORA_MIN_SEG;
		public const string FORMATO_FECHA_HORA_MINUCULA = FORMATO_SOLO_FECHA_MINUCULA + " " + FORMATO_HORA_MIN_SEG;
    public const string FORMATO_FECHA_HORA_CON_GUION_FILESSYTEM = FORMATO_SOLO_FECHA_CON_GUION + " " + FORMATO_HORA_MIN_SEG_FILESYSTEM;
    public const string FORMATO_FECHA_HORA_MINUCULA_CON_GUION = FORMATO_SOLO_FECHA_MINUCULA_CON_GUION + " " + FORMATO_HORA_MIN_SEG;
    public const string FRASE_STR_FORMAT_BASE_FECHA_GENERACION_REPORTE = "Generado al {0:dd-MM-yyyy}";
    public const string SI = "Sí";
		public const string NO = "No";
		public const int MILLISEGS_EN_UN_SEG = 1000;
		public static readonly DateTime FECHA_NO_SETEADA = DateTime.MinValue.AddYears(1900).ToLocalTime().Date;
		public const byte ID_ENERGETICO_TODOS = byte.MaxValue;
    public const byte ID_ENERGETICO_NINGUNO = 0;
    public const string NOMBRE_ENERGETICO_TODOS = "Todos";
		public bool Active { get; set; }
		public string SubRutaPlantillaExcel { get; set; }
		public string NombreProcedimientoAlmacenado { get; set; }
		public string RutaCompletaCSV { get; set; }
		public string ArchivoPlantillaExcel { get; set; }
		public int FilaInicialExcelIteracionDatos { get; set; }
		public string ColumnaInicialExcelIteracionDatos { get; set; }
		public string[] ColumnasNombresCamposExcel { get; set; }
		public string[] ColumnasNombresCamposExcelConDefaultSinInfo { get; set; }
	}
}
