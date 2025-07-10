using System.Collections.Generic;

namespace OrquestadorGesp.AppSettingsJson
{
  public class ConfReporteControlDeCarga : ConfReporteConInsumoRepExt
  {
    public const string PATRON_FECHA_GENERACION_EXCEL_DISPLAY = "{dd-mm-yyyy}";
    public const string FRASE_GENERACION_FECHA_REGEX_PATTERN = "al ([0-9]{2}-[0-9]{2}-[0-9]{4})";
    public const string FRASE_FUSION_MEDIDORES = "Fusion de medidores";
    public const int LARGO_PRIMERA_DIMENSION_ARR_ESTILOS = 2;
    public const byte MES_INICIAL = 11;
    public const byte MES_FINAL = 10;
    public const byte CANT_MESES = 12;
    public readonly HashSet<string> NOMBRES_COLS_EXCELS_NUMEROS = new HashSet<string>() { "RegionId" };
    public readonly Dictionary<bool, string> REPORTA_PMG_BOOL_TO_STR = new Dictionary<bool, string>() { { false, "No_Reporta" }, { true, "Reporta" } };
    public string CeldaNombreServicioExcel { get; set; }
    public string CeldaFraseFechaGeneracion { get; set; }  //para hojas Resumen y Resumen edificio
    public string ColumnaInicialExcelIteracionMeses { get; set; }
    public string ColumnaInicialExcelIteracionResumen { get; set; }
    public string ColumnaMesesCompletosReportados { get; set; }
    public string ColumnaAlMenosDoceMesesCompletosReportados { get; set; }
    public int FilaInicialExcelIteracionResumen { get; set; }
    public string CeldaNombreServicioExcelHojasEdificioMedidor { get; set; }
    public string CeldaNombreServicioExcelHojaResumen { get; set; }
    public string CeldaCantEdificiosQueReportanResumen { get; set; }
    public string ColorFondoTodasComprasValidadas { get; set; }
    public string ColorLetraTodasComprasValidadas { get; set; }
    public string ColorFondoAlgunasComprasSinRevision { get; set; }
    public string ColorLetraAlgunasComprasSinRevision { get; set; }
    public string ColorFondoAlgunasComprasObservadas { get; set; }
    public string ColorLetraAlgunasComprasObservadas { get; set; }
    public string ColorFondoNO { get; set; }
    public string ColorLetraNO { get; set; }
    public string ColorFondoNada { get; set; }
    public string ColorLetraNada { get; set; }
    public string ColorFondoDefault { get; set; }
    public string ColorLetraDefault { get; set; }
    public string FormulaMesesCompletadosReportados { get; set; }
    public string FormulaAlMenos12MesesCompletados { get; set; }
    public string ValorEstadoValidacionOk { get; set; }
    public string ValorEstadoValidacionObservado { get; set; }
    public string ValorEstadoValidacionSinRevision { get; set; }
    public string ValorCasillaDivMedidorEnerMesOK { get; set; }
    public string ValorCasillaDivMedidorEnerMesNO { get; set; }
    public string ValorCasillaDivMedidorEnerMesNoap { get; set; }
    public string ValorCasillaDivMedidorEnerMesNADA { get; set; }

    public string ValorReportaPMG { get; set; }
    public string ValorNoReportaPMG { get; set; }
    public float PorcentajeMinimoParaCasillaOkHojaResumenMedidor { get; set; }
  }
}