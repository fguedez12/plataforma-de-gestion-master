using OrquestadorGesp.ContextEFNetCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using OrquestadorGesp.AppSettingsJson;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using OrquestadorGesp.Helpers;
using Serilog;
using System.Collections.Generic;
using OrquestadorGesp.DTOs;
using System.Text;

namespace OrquestadorGesp.Reportes
{
  public class ReporteControlDeCarga : ReporteBaseExtendidoComoInsumo<RegistroInsumoRepExtControlCarga> //ReporteBase
  {
    private List<ReporteControlDeCargaEnt> registrosReporteControlCargaSP = new List<ReporteControlDeCargaEnt>();
    //private List<RegistroInsumoRepExtControlCarga> registrosReporteControlCargaInsumo = new List<RegistroInsumoRepExtControlCarga>();
    //private ICellStyle estiloCeldaProrrateo = null;
    private ConfReporteControlDeCarga configReporteControlCarga;
    private int intervaloTiempoReintentoEncontrarAlgunArchivoRepExtSegs = 1;
    private int columnaInicialRecuadro = -1;
    private int columnaMesesCompletosReportados = -1;
    private int columnaAlMenosDoceMesesCompletosReportados = -1;
    private int cantRegistrosListaIzq = 0;
    //private HashSet<string> propsNumericasExcel;
    private int anhoInicialRepControlCarga = -1;
    private int anhoFinalRepControlCarga = -1;
    private readonly string NOMBRE_MES_ENERO = DateTimeExtensions.NombreDelMes(1);
    private string colorFondoOKTodasComprasValidadas;
    private string colorLetraOKTodasComprasValidadas;
    private string colorFondoOKAlgunasComprasValidadas;
    private string colorLetraOKAlgunasComprasValidadas;
    private string colorFondoNO;
    private string colorLetraNO;
    private string colorFondoDefault;
    private string colorLetraDefault;
    private string formulaMesesCompletadosReportados;
    private string formulaAlMenos12MesesCompletados;
    private string STR_VAL_OK;
    private string STR_VAL_NO;
    private string STR_VAL_Noap;
    private string STR_VAL_NADA;
    private ICellStyle estiloCeldaPorDefecto;
    private IFont fuentePorDefecto;
    ICellStyle estiloCeldaTodasComprasObsOk;
    IFont fuenteTodasComprasObsOk;
    ICellStyle estiloCeldaHayComprasSinRev;
    IFont fuenteHayComprasSinRev;
    ICellStyle estiloCeldaHayComprasConObs;
    IFont fuenteHayComprasConObs;
    ICellStyle estiloCeldaNO;
    IFont fuenteNO;
    ICellStyle estiloCeldaNada;
    IFont fuenteNada;

    private DateTime[] arrFechasCadaMesAnhoMovil;
    /*
				protected string strCeldaFraseFechaGeneracion; 
				protected string strCeldaNombreServicio;      
				protected string strMsj = string.Empty;
				protected Dictionary<short, string> nombresEnergeticos = new Dictionary<short, string>();
				protected Dictionary<long, bool> dictServiciosGenerados = new Dictionary<long, bool>();
				protected Dictionary<string, string> columnasRepExtInsumo = new Dictionary<string, string>();
				protected string formatoFechaDisplay;
				protected string formatoFechaGeneracionReporte;¿?
				protected string strComentarioFechaMod;
				protected int nroColumnaFraseFechaGeneracion = -1;
				protected int nroFilaFraseFechaGeneracion = -1;
				protected int cantidadExcelsGenerados = -1;
				protected string columnaInicialIteracion;
				protected string subRutaReporteOrigen;
				protected string rutaPlantillasReportes;
				protected string fileNameExcelRepExt;
				protected FileInfo fileInfoExcelRepExt;
				protected int cantTiempoMaximoSecsEsperandoAlgunArchivoRepExt = -1;
				protected int tiempoTranscurridoEsperandoAlgunArchivoRepExt = -1;
				protected int intervaloTiempoReintentoEncontrarAlgunArchivoRepExtMseg = -1;
				protected string FRASE_GENERACION_FECHA_REGEX_PATTERN;
				protected ConfReporteExtendido configReporteExtendido;
				protected List<DTO> conjuntoComprasProrrateadas;

		 */
    public ReporteControlDeCarga() : base()
    {
      LogicaComunConstructor();
    }

    private void LogicaComunConstructor()
    {
    }
    //private readonly string formatoColumnaStr = "Propiedad {0} = [[{1}]]";
    //private readonly string formatoCeldaStr = "Celda {0}{1} es Nula? {2}";

    public override void InicializarEstructurasDatosReporte()
    {
      //todos estos var van a ser herencia de ReporteBaseExtendidoComoInsumo
      subRutaReporteOrigenOrigRepExt = configReporteExtendido.SubRutaPlantillaExcel;
      strCeldaFraseFechaGeneracion = configReporteControlCarga.CeldaFraseFechaGeneracion;
      strCeldaNombreServicio = configReporteControlCarga.CeldaNombreServicioExcel;
      celRef = new CellReference(strCeldaFraseFechaGeneracion);
      nroColumnaFraseFechaGeneracion = celRef.Col;
      nroFilaFraseFechaGeneracion = celRef.Row;
      columnasRepExtInsumo = configReporteControlCarga.ColumnasRepExtInsumo;
      formatoFechaDisplay = ConfReporteBase.FORMATO_SOLO_FECHA;
      formatoFechaGeneracionReporte = ConfReporteBase.FORMATO_SOLO_FECHA_CON_GUION;
      intervaloTiempoReintentoEncontrarAlgunArchivoRepExtMseg = configReporteControlCarga.IntervaloTiempoCheckeoExistenciaArchivosExcelRepExtSegs;
      cantTiempoMaximoSecsEsperandoAlgunArchivoRepExt = configReporteControlCarga.TiempoEsperaExistenciaAlgunArchivoExcelRepExtSegs;
      fraseGeneracionFechaRegexPattern = ConfReporteControlDeCarga.FRASE_GENERACION_FECHA_REGEX_PATTERN;
      colorFondoOKTodasComprasValidadas = configReporteControlCarga.ColorFondoTodasComprasValidadas;
      colorLetraOKTodasComprasValidadas = configReporteControlCarga.ColorLetraTodasComprasValidadas;
      colorFondoOKAlgunasComprasValidadas = configReporteControlCarga.ColorFondoAlgunasComprasSinRevision;
      colorLetraOKAlgunasComprasValidadas = configReporteControlCarga.ColorLetraAlgunasComprasSinRevision;
      colorFondoNO = configReporteControlCarga.ColorFondoNO;
      colorLetraNO = configReporteControlCarga.ColorLetraNO;
      colorFondoDefault = configReporteControlCarga.ColorFondoDefault;
      colorLetraDefault = configReporteControlCarga.ColorLetraDefault;
      formulaMesesCompletadosReportados = configReporteControlCarga.FormulaMesesCompletadosReportados;
      if (string.IsNullOrEmpty(formulaMesesCompletadosReportados))
      {
        strMsj = "La constante \"FormulaMesesCompletadosReportados\" de configuracion json reporte de carga no existe o viene vacia";
        Console.WriteLine(strMsj);
        Log.Error(strMsj);
        TerminarGeneracionReporte();
      }
      formulaAlMenos12MesesCompletados = configReporteControlCarga.FormulaAlMenos12MesesCompletados;
      if (string.IsNullOrEmpty(formulaAlMenos12MesesCompletados))
      {
        strMsj = "La constante \"FormulaAlMenos12MesesCompletados\" de configuracion json reporte de carga no existe o viene vacia";
        Console.WriteLine(strMsj);
        Log.Error(strMsj);
        TerminarGeneracionReporte();
      }
      if (formulaMesesCompletadosReportados.StartsWith('=')) formulaMesesCompletadosReportados = formulaMesesCompletadosReportados.Substring(1);
      if (formulaAlMenos12MesesCompletados.StartsWith('=')) formulaAlMenos12MesesCompletados = formulaAlMenos12MesesCompletados.Substring(1);
      //formulaMesesCompletadosReportados = formulaMesesCompletadosReportados.Replace(',', '~');
      //formulaMesesCompletadosReportados = formulaMesesCompletadosReportados.Replace(';', ',');
      //formulaMesesCompletadosReportados = formulaMesesCompletadosReportados.Replace('~', ',');
      //formulaAlMenos12MesesCompletados = formulaAlMenos12MesesCompletados.Replace(',', '~');
      //formulaAlMenos12MesesCompletados = formulaAlMenos12MesesCompletados.Replace(';', ',');
      //formulaAlMenos12MesesCompletados = formulaAlMenos12MesesCompletados.Replace('~', ',');
      STR_VAL_OK = configReporteControlCarga.ValorCasillaDivMedidorEnerMesOK;
      STR_VAL_NO = configReporteControlCarga.ValorCasillaDivMedidorEnerMesNO;
      STR_VAL_Noap = configReporteControlCarga.ValorCasillaDivMedidorEnerMesNoap;
      STR_VAL_NADA = configReporteControlCarga.ValorCasillaDivMedidorEnerMesNADA;
    }

    public override void ObtenerConfReporte()
    {
      ConfReporte = CurrConfGlobal.ReporteControlDeCarga;
      configReporteControlCarga = (ConfReporteControlDeCarga)ConfReporte;
      configReporteExtendido = CurrConfGlobal.ReporteExtendido;
      intervaloTiempoReintentoEncontrarAlgunArchivoRepExtSegs = configReporteControlCarga.IntervaloTiempoCheckeoExistenciaArchivosExcelRepExtSegs;
      intervaloTiempoReintentoEncontrarAlgunArchivoRepExtMseg = intervaloTiempoReintentoEncontrarAlgunArchivoRepExtSegs
        * ConfReporteBase.MILLISEGS_EN_UN_SEG;
      columnaInicialRecuadro = CellReference.ConvertColStringToIndex(configReporteControlCarga.ColumnaInicialExcelIteracionMeses);
      columnaMesesCompletosReportados = CellReference.ConvertColStringToIndex(configReporteControlCarga.ColumnaMesesCompletosReportados);
      columnaAlMenosDoceMesesCompletosReportados = CellReference.ConvertColStringToIndex(configReporteControlCarga.ColumnaAlMenosDoceMesesCompletosReportados);
      anhoFinalRepControlCarga = fechaHoy.Year;
      anhoInicialRepControlCarga = anhoFinalRepControlCarga - 1;
      cantMeses = ConfReporteControlDeCarga.CANT_MESES;
      arrFechasCadaMesAnhoMovil = new DateTime[cantMeses];

      DateTime dtIni = new DateTime(DateTime.Now.Year - 1, ConfReporteControlDeCarga.MES_INICIAL, 1);
      for (int contMes = 0; contMes < cantMeses; contMes++)
      {
        arrFechasCadaMesAnhoMovil[contMes] = dtIni.AddMonths(contMes);
      }
    }

    public override void ObtenerDatosSP()
    {
      if (RegistroServIter == null) return;
      //if (EstadoProcesoReporte >= PROCESO_REP_INICIADO) return;
      //if (EstadoProcesoReporte >= PROCESO_REP_EN_PROCESO) return;
      var commandStr = string.Format(PLANTILLA_COD_SQL_EJECUTAR_SP_SIN_PARAMS, string.Format(nombreSP, RegistroServIter.Int64PK));
      var msjCommandStr = string.Format("<{0}>. Comando SQL A Ejecutar=\"{1}\". Estado Reporte:{2}", GetType().Name, commandStr, EstadoProcesoReporte);
      Console.WriteLine(msjCommandStr);
      Log.Information(msjCommandStr);
      // FALLA:
      // falla obtencion de datos de SP SP_REPORTE_COMPACTO porque esta mapeado una propiedad que no puede ser nula y viene nula
      registrosReporteControlCargaSP = DbContextInstance.ReporteControlDeCargaSet.FromSql(commandStr).ToList();
      strMsj = string.Format("{0} => cant registrosReporteControlCargaSP = {1}", GetType().Name, registrosReporteControlCargaSP.Count);
      cantRegistrosListaIzq = registrosReporteControlCargaSP.Count;
      strMsj = string.Format("{0} => cantRegistrosListaIzq = {1}", GetType().Name, cantRegistrosListaIzq);
      //EstadoProcesoReporte = PROCESO_REP_INICIADO;
    }

    public override void ObtenerDatosCSV()
    {
    }

    private void SetearColorFondoConColorFuenteEstiloCelda(ICellStyle estiloCelda, IFont fuente, string colorFondo, string colorFuente)
    {
      fuente.Color = IndexedColors.ValueOf(colorFuente).Index;
      //cellStyleCasillaNormal.CloneStyleFrom(cell.CellStyle);
      estiloCelda.BorderTop = BorderStyle.Thin;
      estiloCelda.BorderBottom = BorderStyle.Thin;
      estiloCelda.BorderLeft = BorderStyle.Thin;
      estiloCelda.BorderRight = BorderStyle.Thin;
      estiloCelda.FillForegroundColor = IndexedColors.ValueOf(colorFondo).Index;
      estiloCelda.FillPattern = FillPattern.SolidForeground;
      estiloCelda.SetFont(fuente);

    }

    public override void LogicaReporte()
    {
      //List<ReporteControlDeCargaEnt> registrosReporteControlCargaIterSrvSP = registrosReporteControlCargaSP;
      XSSFSheet hojaResumenMedidor = (XSSFSheet)PlantillaXltxWb.GetSheetAt(2);
      XSSFSheet hojaResumenEdificio = (XSSFSheet)PlantillaXltxWb.GetSheetAt(1);
      XSSFSheet hojaResumen = (XSSFSheet)PlantillaXltxWb.GetSheetAt(0);
      DateTime dateTimeIter = arrFechasCadaMesAnhoMovil[0];
      int rowNum = 0;
      int colNum = 0;
      StringBuilder sb = new StringBuilder();
      //strMsj = string.Format("{0} => # registrosReporteControlCargaIterSrvSP={1} cantRegistrosListaIzq={2}, mesInicialRepControlCarga={3}, mesFinalRepControlCarga={4}"
      //    , GetType().Name, registrosReporteControlCargaIterSrvSP.Count, cantRegistrosListaIzq, mesInicialRepControlCarga, mesFinalRepControlCarga);
      //Console.WriteLine(strMsj);
      //Log.Information(strMsj);

      celRef = new CellReference(configReporteControlCarga.CeldaNombreServicioExcelHojasEdificioMedidor);
      row = hojaResumenMedidor.GetRow(celRef.Row);
      cell = row.GetCell(celRef.Col);
      cell.SetCellValue(RegistroServIter.NombreServicio);
      row = hojaResumenEdificio.GetRow(celRef.Row);
      cell = row.GetCell(celRef.Col);
      cell.SetCellValue(RegistroServIter.NombreServicio);
      celRef = new CellReference(configReporteControlCarga.CeldaNombreServicioExcelHojaResumen);
      row = hojaResumen.GetRow(celRef.Row);
      cell = row.GetCell(celRef.Col);
      cell.SetCellValue(RegistroServIter.NombreServicio);

      strMsj = string.Format(ConfReporteBase.FRASE_STR_FORMAT_BASE_FECHA_GENERACION_REPORTE, DateTime.Now);
      celRef = new CellReference(configReporteControlCarga.CeldaFraseFechaGeneracion);
      row = hojaResumen.GetRow(celRef.Row);
      cell = row.GetCell(celRef.Col);
      cell.SetCellValue(strMsj);


      if (registrosReporteControlCargaSP.Count == 0)
      {
        strMsj = string.Format("No hay registros de compras entre el {0:dd 'de' MMMM 'de' yyyy} y el {1:dd 'de' MMMM 'de' yyyy}\npara el servicio \"{2}\" (id={3}).\nDejando reporte \"{4}\" Vacio\n"
          , arrFechasCadaMesAnhoMovil[0], arrFechasCadaMesAnhoMovil[arrFechasCadaMesAnhoMovil.Length - 1], RegistroServIter.NombreServicio, RegistroServIter.Int64PK, GetType().Name);
        Console.WriteLine(strMsj);
        Log.Information(strMsj);
        TerminarGeneracionReporte();
        return;
      }


      var medidorCompraListToCambioId = registrosReporteControlCargaSP.Where(r => r.CambioId > 0)
        .Select(r => new Tuple<long,long, string>( r.CambioId, r.MedidorDivisionId, r.NroMedidorDivision)).Distinct().ToList();
      var medidorAntiguoListToCambioId = registrosReporteControlCargaSP.Where(r => r.CambioId > 0).Where(r => r.MedidorAntiguoId > 0)
        .Select(r => new Tuple<long, long, string>(r.CambioId, r.MedidorAntiguoId, r.NroMedidorAntiguo)).Distinct().ToList();
      var medidorNuevoListToCambioId = registrosReporteControlCargaSP.Where(r => r.CambioId > 0).Where(r => r.MedidorNuevoId > 0)
        .Select(r => new Tuple<long, long, string>(r.CambioId, r.MedidorNuevoId, r.NroMedidorNuevo)).Distinct().ToList();
      //List<Tuple<long, long, string>> medidorIdListToCambioId = new List<Tuple<long, long, string>>();
      //medidorIdListToCambioId.AddRange(medidorCompraListToCambioId);
      //medidorIdListToCambioId.AddRange(medidorAntiguoListToCambioId);
      //medidorIdListToCambioId.AddRange(medidorNuevoListToCambioId);
      //medidorCompraListToCambioId = medidorCompraListToCambioId.Distinct();
      Dictionary<long, string> cambioNroMedidorCompuesto = new Dictionary<long, string>();
      foreach(var itemMedidorDivCambio in medidorCompraListToCambioId)
      {
        
        long idCambio = itemMedidorDivCambio.Item1;
        IEnumerable<Tuple<long, long, string>> tuplasMedidorAntiguo = medidorAntiguoListToCambioId.Where(a => a.Item1 == idCambio);
        IEnumerable<Tuple<long, long, string>> tuplasMedidorNuevo = medidorNuevoListToCambioId.Where(a => a.Item1 == idCambio);
        sb.Clear();
        strMsj = string.Join("; ", tuplasMedidorAntiguo.Select(t => t.Item3));
        sb.Append(strMsj);
        strMsj = " > ";
        sb.Append(strMsj);
        strMsj = string.Join("; ", tuplasMedidorNuevo.Select(t => t.Item3));
        sb.Append(strMsj);
        cambioNroMedidorCompuesto[idCambio] = sb.ToString();
        //cambioNroMedidorCompuesto[itemMedidorDivCambio.Item1] 
      }
      var medidorIdListToCambioId = medidorCompraListToCambioId.Union(medidorAntiguoListToCambioId);
      medidorCompraListToCambioId.Clear();
      medidorAntiguoListToCambioId.Clear();
      medidorIdListToCambioId = medidorIdListToCambioId.Union(medidorNuevoListToCambioId);
      medidorNuevoListToCambioId.Clear();
      medidorCompraListToCambioId = null;
      medidorAntiguoListToCambioId = null;
      medidorCompraListToCambioId = null;
      Dictionary<long, long> mapeoCambioMedidor = new Dictionary<long, long>();
      foreach(var regCambioMed in medidorIdListToCambioId)
      {
        mapeoCambioMedidor[regCambioMed.Item2] = regCambioMed.Item1;
        strMsj = string.Format("mapeoCambioMedidor[{0}]={1}", regCambioMed.Item2, regCambioMed.Item1);
        Console.WriteLine(strMsj);
        Log.Information(strMsj);
      }

      List<ReporteControlDeCargaSPAgrupado> registrosSPControlCargaSrv = registrosReporteControlCargaSP.GroupBy(g => new {
        g.DivisionId, g.Division, g.ReportaPMG, g.RegionId, g.RegionPos, g.Comuna, g.NroEmpresaDistId, g.EmpresaDistribuidora
        , NroClienteIdComb = g.CambioId > 0 ? -1L : g.NroClienteId, NroClienteComb = g.CambioId > 0 ? ConfReporteControlDeCarga.FRASE_FUSION_MEDIDORES : g.NroCliente
        , g.CambioId, NumeroMedidorUnico = (g.CambioId > 0 ? cambioNroMedidorCompuesto[g.CambioId] : g.NroMedidorDivision) 
        , IdMedidorSiEsQueEsUnico = g.CambioId > 0 ? g.CambioId : g.MedidorDivisionId
        //,
        //IdMedidoresNuevos = g.CambioId > 0 ? string.Join(",", g.MedidorNuevoId) : string.Empty
        //,
        //IdMedidoresAntiguos = g.CambioId > 0 ? string.Join(",", g.MedidorAntiguoId) : string.Empty
        , g.Energetico, g.EnergeticoId
        //, IdMedidorSiQueEsUnico = g.CambioId > 0 ? Array.Fill(Array.Empty<long>(), g.MedidorAntiguoId) : [g.MedidorDivisionId], g.Energetico, g.EnergeticoId
      })
        .Select(r => new ReporteControlDeCargaSPAgrupado( r.Key.DivisionId, r.Key.Division, r.Key.ReportaPMG, r.Key.RegionId
        , r.Key.RegionPos, r.Key.Comuna, r.Key.NroEmpresaDistId, r.Key.EmpresaDistribuidora, r.Key.NroClienteIdComb
        , r.Key.NroClienteComb, r.Key.CambioId
        , r.Key.NumeroMedidorUnico 
        , r.Key.IdMedidorSiEsQueEsUnico, r.Key.Energetico, r.Key.EnergeticoId)).ToList();
      var nroColumnaInicialRepExt = CellReference.ConvertColStringToIndex(configReporteExtendido.ColumnaInicialExcelIteracionDatos);
      var nroFilaInicialRepExt = configReporteExtendido.FilaInicialExcelIteracionDatos - 1;
      //List<RegistroInsumoRepExtControlCarga> registrosInsumoRepExtControlCarga = ExcelHelper.ObtenerInfoDesdeInsumoXlsx<RegistroInsumoRepExtControlCarga>(fileNameExcelRepExt
      //                                                                            , nroColumnaInicialRepExt, nroFilaInicialRepExt
      //                                                                            , configReporteControlCarga.ColumnasRepExtInsumo, 0
      //                                                                            , configReporteExtendido.ColumnasNombresCamposExcel.Length - 1);
      //colNum = CellReference.ConvertColStringToIndex(configReporteControlCarga.ColumnaInicialExcelIteracionMeses);
      //rowNum = configReporteControlCarga.FilaInicialExcelIteracionDatos - 1;
      //cell = hojaResumenMedidor.GetRow(rowNum).GetCell(colNum);
      //var registrosInsumoRepExtControlCarga = conjuntoComprasProrrateadas;

      estiloCeldaPorDefecto = PlantillaXltxWb.CreateCellStyle();
      fuentePorDefecto = PlantillaXltxWb.CreateFont();
      SetearColorFondoConColorFuenteEstiloCelda(estiloCeldaPorDefecto, fuentePorDefecto, configReporteControlCarga.ColorFondoDefault, configReporteControlCarga.ColorLetraDefault);

      estiloCeldaTodasComprasObsOk = PlantillaXltxWb.CreateCellStyle();
      fuenteTodasComprasObsOk = PlantillaXltxWb.CreateFont();
      SetearColorFondoConColorFuenteEstiloCelda(estiloCeldaTodasComprasObsOk, fuenteTodasComprasObsOk, configReporteControlCarga.ColorFondoTodasComprasValidadas, configReporteControlCarga.ColorLetraTodasComprasValidadas);

      estiloCeldaHayComprasSinRev = PlantillaXltxWb.CreateCellStyle();
      fuenteHayComprasSinRev = PlantillaXltxWb.CreateFont();
      SetearColorFondoConColorFuenteEstiloCelda(estiloCeldaHayComprasSinRev, fuenteHayComprasSinRev, configReporteControlCarga.ColorFondoAlgunasComprasSinRevision, configReporteControlCarga.ColorLetraAlgunasComprasSinRevision);

      estiloCeldaHayComprasConObs = PlantillaXltxWb.CreateCellStyle();
      fuenteHayComprasConObs = PlantillaXltxWb.CreateFont();
      SetearColorFondoConColorFuenteEstiloCelda(estiloCeldaHayComprasConObs, fuenteHayComprasConObs, configReporteControlCarga.ColorFondoAlgunasComprasObservadas, configReporteControlCarga.ColorLetraAlgunasComprasObservadas);

      estiloCeldaNO = PlantillaXltxWb.CreateCellStyle();
      fuenteNO = PlantillaXltxWb.CreateFont();
      SetearColorFondoConColorFuenteEstiloCelda(estiloCeldaNO, fuenteNO, configReporteControlCarga.ColorFondoNO, configReporteControlCarga.ColorLetraNO);

      estiloCeldaNada = PlantillaXltxWb.CreateCellStyle();
      fuenteNada = PlantillaXltxWb.CreateFont();
      SetearColorFondoConColorFuenteEstiloCelda(estiloCeldaNada, fuenteNada, configReporteControlCarga.ColorFondoNada, configReporteControlCarga.ColorLetraNada);
      //strMsj = string.Format("1ro --> CANTIDAD registrosInsumoRepExtControlCarga={0}", registrosInsumoRepExtControlCarga.Count);
      //Console.WriteLine(strMsj);
      //Log.Information(strMsj);

      conjuntoComprasProrrateadas.RemoveAll(r => r.TipoTransaccion == configReporteExtendido.ValorTipoTransaccionCompra);// List<RegistroDataProcesadaControlCarga> 
      foreach (var compraProrrateada in conjuntoComprasProrrateadas)
      {
        if (compraProrrateada.IdNumeroDeCliente < 0)
        {
          compraProrrateada.EnergeticoId = 0;
          compraProrrateada.IdMedidor = -1L;
          continue;
        }
        if (mapeoCambioMedidor.ContainsKey(compraProrrateada.IdMedidor))
        {
          compraProrrateada.IdMedidor = mapeoCambioMedidor[compraProrrateada.IdMedidor];
        }
      }
      //strMsj = string.Format("2do --> CANTIDAD registrosInsumoRepExtControlCarga SIN Tipo Trans->Compra = {0}", registrosInsumoRepExtControlCarga.Count);
      //Console.WriteLine(strMsj);
      //Log.Information(strMsj);

      List<RegistroDataProcesadaControlCarga> registrosDataPreProcesadaInsumoRepExt = conjuntoComprasProrrateadas.GroupBy(g =>
                                                              new
                                                              {
                                                                g.IdDivisionCompra
                                                                ,
                                                                MesFinLectura = g.FinLectura.Month
                                                                ,
                                                                AnhoFinLectura = g.FinLectura.Year
                                                                ,
                                                                //,
                                                                //MesInicioLectura = g.InicioLectura.Month
                                                                //,
                                                                //AnhoInicioLectura = g.InicioLectura.Year
                                                                //,
                                                                g.IdNumeroDeCliente
                                                                ,
                                                                g.IdMedidor
                                                                ,
                                                                g.EstadoValidacion
                                                                ,
                                                                g.EnergeticoId
                                                              })
                                                              .Select(x => new RegistroDataProcesadaControlCarga(
                                                                  x.Key.IdDivisionCompra
                                                                  , x.Key.EstadoValidacion
                                                                  , Convert.ToByte(x.Key.MesFinLectura)
                                                                  , x.Key.AnhoFinLectura
                                                                  , x.Key.IdNumeroDeCliente
                                                                  , x.Key.IdMedidor
                                                                  , x.Key.EstadoValidacion
                                                                  , x.Key.EnergeticoId
                                                                  , x.Sum(r => (r.FinLectura - r.InicioLectura).TotalDays))).ToList();

      strMsj = string.Format("1ro) --> CANTIDAD registrosDataPreProcesadaInsumoRepExt = {0}", registrosDataPreProcesadaInsumoRepExt.Count);
      Console.WriteLine(strMsj);
      Log.Information(strMsj);
      strMsj = string.Format("2do) --> CANTIDAD registrosDataPreProcesadaInsumoRepExtSinNroCliente = {0}", registrosDataPreProcesadaInsumoRepExt.Where( n => n.IdNumeroDeCliente < 0 ).Count());
      Console.WriteLine(strMsj);
      Log.Information(strMsj);
      strMsj = string.Format("3ro) --> CANTIDAD registrosDataPreProcesadaConEnergeticoCero = {0}", registrosDataPreProcesadaInsumoRepExt.Where(e => e.EnergeticoId == 0).Count());
      Console.WriteLine(strMsj);
      Log.Information(strMsj);
      List<RegistroDataProcesadaControlCarga> registrosDataCuadriculaHojaMedidores = new List<RegistroDataProcesadaControlCarga>();
      float cantidadDiasMesIter = 0;
      
      sb.Clear();
      cantRegistrosListaIzq = registrosSPControlCargaSrv.Count;
      foreach (ReporteControlDeCargaSPAgrupado repControlCargaSPIter in registrosSPControlCargaSrv)
      {
        int anhoIter = anhoInicialRepControlCarga;
        bool esNoap = !repControlCargaSPIter.ReportaPMG;
        long cambioId = repControlCargaSPIter.CambioId;
        //strMsj = string.Format(" [DivId={0}|CliId={1}|CmbId={2}]"
        //  , repControlCargaSPIter.DivisionId, repControlCargaSPIter.NroClienteId, repControlCargaSPIter.CambioId) ;
        //repControlCargaSPIter.Energetico += strMsj;
        //long medidorIdTraducidoSp = repControlCargaSPIter.MedidorDivisionId;
        sb.Clear();
        strMsj = string.Format("repControlCargaSPIter->[MedidorDivisionId={0,7};", repControlCargaSPIter.IdMedidor);
        sb.Append(strMsj);
        strMsj = string.Format("CambioId={0,4};", repControlCargaSPIter.CambioId);
        sb.Append(strMsj);

        //se cambia el medidorId por el IdCambio
        if (cambioId > 0)
        {
          //medidorIdTraducidoSp = mapeoCambioMedidor[repControlCargaSPIter.MedidorDivisionId];
          strMsj = string.Format("medidorIdConCambioSp={0,7};", repControlCargaSPIter.IdMedidor);
          sb.Append(strMsj);
        } else
        {
          strMsj = string.Format("medidorIdSinCambioSp={0,7};", repControlCargaSPIter.IdMedidor);
          sb.Append(strMsj);
        }
        //strMsj = string.Format("4to) --> repControlCarga->[NroMedidorId={0,-9};repControlCargaSPIter.ReportaPMG={1}]=>esNoap:{2}"
        //  , repControlCargaSPIter.NroMedidorId, Convert.ToInt16(repControlCargaSPIter.ReportaPMG), Convert.ToInt16(esNoap));
        strMsj = string.Format("EnergeticoId={0,3};", repControlCargaSPIter.EnergeticoId);
        sb.Append(strMsj);


        for (int indexMes = 0; indexMes < cantMeses; indexMes++)
        {
          RegistroDataProcesadaControlCarga registroDataCuadriculaHojaMedidores = new RegistroDataProcesadaControlCarga();
          dateTimeIter = arrFechasCadaMesAnhoMovil[indexMes];
          cantidadDiasMesIter = Convert.ToSingle(dateTimeIter.UltimoDiaDelMes().Day);
          registroDataCuadriculaHojaMedidores.Anho = dateTimeIter.Year;
          registroDataCuadriculaHojaMedidores.Mes = Convert.ToByte(dateTimeIter.Month);
          registroDataCuadriculaHojaMedidores.TipoTransaccion = configReporteExtendido.ValorTipoTransaccionConsumo;
          registroDataCuadriculaHojaMedidores.IdDivisionCompra = Convert.ToInt32(repControlCargaSPIter.DivisionId);
          registroDataCuadriculaHojaMedidores.EnergeticoId = Convert.ToByte(repControlCargaSPIter.EnergeticoId);
          registroDataCuadriculaHojaMedidores.IdMedidor = Convert.ToInt32(repControlCargaSPIter.IdMedidor);
          registroDataCuadriculaHojaMedidores.CantDiasAcumuladosConsumoMesAnhoDivMed = 0;
          //registroDataCuadriculaHojaMedidores.IdMedidor = Convert.ToInt32(repControlCargaSPIter.MedidorDivisionId);
          //registroDataCuadriculaHojaMedidores.IdMedidor = medidorIdTraducidoSp;

          var regDataPreprocesadaInsumoRepExt = registrosDataPreProcesadaInsumoRepExt
              .Where(r => r.IdDivisionCompra == registroDataCuadriculaHojaMedidores.IdDivisionCompra);
          regDataPreprocesadaInsumoRepExt = regDataPreprocesadaInsumoRepExt.Where(r => r.EnergeticoId == registroDataCuadriculaHojaMedidores.EnergeticoId);
          regDataPreprocesadaInsumoRepExt = regDataPreprocesadaInsumoRepExt.Where(r => r.IdMedidor == registroDataCuadriculaHojaMedidores.IdMedidor);
          if (indexMes == 0)
          {
            if (regDataPreprocesadaInsumoRepExt != null)
            {
              strMsj = string.Format("cantRepPrepExt->IdMedidor {0,7}={1,6};"
                , registroDataCuadriculaHojaMedidores.IdMedidor
                , regDataPreprocesadaInsumoRepExt.Count());
              sb.Append(strMsj);
            }
            else
            {
              strMsj = string.Format("[!] RepPrepExt->IdMedidor {0,7} NULO!!;", registroDataCuadriculaHojaMedidores.IdMedidor);
              sb.Append(strMsj);
            }
          }
          regDataPreprocesadaInsumoRepExt = regDataPreprocesadaInsumoRepExt.Where(r => r.Anho == registroDataCuadriculaHojaMedidores.Anho);
          regDataPreprocesadaInsumoRepExt = regDataPreprocesadaInsumoRepExt.Where(r => r.Mes == registroDataCuadriculaHojaMedidores.Mes);
          //if (cambioId > 0)
          //{
          //  regDataPreprocesadaInsumoRepExt = regDataPreprocesadaInsumoRepExt.Where(r => r.IdMedidor == registroDataCuadriculaHojaMedidores.IdMedidor);
          //}
          //else
          //{
          //}
          //registroDataCuadriculaHojaMedidores.CantDiasAcumuladosConsumoMesAnhoDivMed
          registroDataCuadriculaHojaMedidores.CantDiasAcumuladosConsumoMesAnhoDivMed = regDataPreprocesadaInsumoRepExt.Sum(s => s.CantDiasAcumuladosConsumoMesAnhoDivMed);
          if (indexMes == 0)
          {
            if (regDataPreprocesadaInsumoRepExt != null)
            {
              strMsj = string.Format("Sum->{0,4}-{1,2}={2,3};"
                , registroDataCuadriculaHojaMedidores.Anho, registroDataCuadriculaHojaMedidores.Mes
                , regDataPreprocesadaInsumoRepExt.Sum( d => d.CantDiasAcumuladosConsumoMesAnhoDivMed));
              sb.Append(strMsj);
            }
            else
            {
              strMsj = string.Format("Fec->{0,4}-{1,2}=NUL;"
                , registroDataCuadriculaHojaMedidores.Anho, registroDataCuadriculaHojaMedidores.Mes);
              sb.Append(strMsj);
            }
            strMsj = string.Format("DiasAcum={0,3};", registroDataCuadriculaHojaMedidores.CantDiasAcumuladosConsumoMesAnhoDivMed);
            Console.WriteLine(sb.ToString());
            Log.Information(sb.ToString());
          }
          if (esNoap)
          {
            registroDataCuadriculaHojaMedidores.ValorStrCelda = configReporteControlCarga.ValorCasillaDivMedidorEnerMesNoap;
            registroDataCuadriculaHojaMedidores.EstadoComprasCasilla = (sbyte)EstadoCasillaValor.NOAP;
            registroDataCuadriculaHojaMedidores.EstadoPorcentajeCasilla = (sbyte)EstadoCasillaValor.NOAP;
            //strMsj = string.Format("{0:MM/yyyy}##diasM={1}##C={3};V={2,4}\t"
            //  , dateTimeIter, cantidadDiasMesIter, registroDataCuadriculaHojaMedidores.ValorStrCelda, registroDataCuadriculaHojaMedidores.ColorFondoCelda.Substring(0,1));
            //sb.Append(strMsj);
          }
          else if (regDataPreprocesadaInsumoRepExt != null)
          {
            // en otro caso sería 0 que estan todas las compras en estado validacion ok
            int cantComprasConObsMesDivEner = regDataPreprocesadaInsumoRepExt.Where(r => r.EstadoValidacion == configReporteControlCarga.ValorEstadoValidacionObservado).Count();
            int cantComprasSinRevMesDivEner = regDataPreprocesadaInsumoRepExt.Where(r => r.EstadoValidacion == configReporteControlCarga.ValorEstadoValidacionSinRevision).Count();
            int cantComprasTotales = regDataPreprocesadaInsumoRepExt.Count();
            if (cantComprasConObsMesDivEner > 0)
            {
              registroDataCuadriculaHojaMedidores.EstadoComprasCasilla = (sbyte)EstadoCasillaValor.CON_OBS; //Fondo Rojo (como no se indicó el valor del texto
              //strMsj = string.Format("{0:MM/yyyy}**diasM={1}**C={2}"
              //  ,dateTimeIter, cantidadDiasMesIter, registroDataCuadriculaHojaMedidores.ColorFondoCelda.Substring(0, 1));
              //sb.Append(strMsj);
            }
            else if (cantComprasSinRevMesDivEner > 0)
            {
              registroDataCuadriculaHojaMedidores.EstadoComprasCasilla = (sbyte)EstadoCasillaValor.SIN_REV; //Fondo Blanco (hay alguna compra con
              //registroDataCuadriculaHojaMedidores.ColorLetraCelda = configReporteControlCarga.ColorLetraAlgunasComprasSinRevision; //estado revision "sin revision)
              //strMsj = string.Format("{0:MM/yyyy}--diasM={1}--C={2}"
              //  ,dateTimeIter, cantidadDiasMesIter, registroDataCuadriculaHojaMedidores.ColorFondoCelda.Substring(0, 1));
              //sb.Append(strMsj);
            }
            else if (cantComprasTotales > 0)
            {
              registroDataCuadriculaHojaMedidores.EstadoComprasCasilla = (sbyte)EstadoCasillaValor.OK; // Fondo VERDE (el else, es decir, NO HAY
              //registroDataCuadriculaHojaMedidores.ColorLetraCelda = configReporteControlCarga.ColorLetraTodasComprasValidadas; // revision "observada" ni "sin revision", por lo tanto VERDE
              //strMsj = string.Format("{0:MM/yyyy}++diasM={1}++C={2}"
              //  , dateTimeIter, cantidadDiasMesIter, registroDataCuadriculaHojaMedidores.ColorFondoCelda.Substring(0, 1));
              //sb.Append(strMsj);
            }
            //else
            //{
            //  registroDataCuadriculaHojaMedidores.EstadoComprasCasilla = (sbyte)EstadoCasillaValor.NADA;
            //}

            // WIP: en base a la cantidad de dias cubiertos del mes anho (>= 93%) poner "SI" o "NO"
            float cantDiasAcumuladosMesAnhoDivMedEne = Convert.ToSingle(registroDataCuadriculaHojaMedidores.CantDiasAcumuladosConsumoMesAnhoDivMed);

            if (100.0 * (cantDiasAcumuladosMesAnhoDivMedEne / cantidadDiasMesIter) >= configReporteControlCarga.PorcentajeMinimoParaCasillaOkHojaResumenMedidor)
            {
              registroDataCuadriculaHojaMedidores.ValorStrCelda = configReporteControlCarga.ValorCasillaDivMedidorEnerMesOK;
              registroDataCuadriculaHojaMedidores.EstadoPorcentajeCasilla = (sbyte)EstadoCasillaValor.OK;

              //strMsj = string.Format(";V=[{0,4}]\t"
              //  , registroDataCuadriculaHojaMedidores.ValorStrCelda);
              //sb.Append(strMsj);
            }
            else
            {
              registroDataCuadriculaHojaMedidores.ValorStrCelda = configReporteControlCarga.ValorCasillaDivMedidorEnerMesNO;
              registroDataCuadriculaHojaMedidores.EstadoPorcentajeCasilla = (sbyte)EstadoCasillaValor.NO;
              //strMsj = string.Format(";V=<{0,4}>\t"
              //  , registroDataCuadriculaHojaMedidores.ValorStrCelda);
              //sb.Append(strMsj);
            }
          }
          registrosDataCuadriculaHojaMedidores.Add(registroDataCuadriculaHojaMedidores);
        }
        //Console.WriteLine(sb.ToString());
        //Log.Information(sb.ToString());
        //sb.Clear();
      }
      //strMsj = string.Format("a) --> CANTIDAD registrosDataCuadriculaHojaMedidores = {0}", registrosDataCuadriculaHojaMedidores.Count);
      //Console.WriteLine(strMsj);
      //Log.Information(strMsj);
      //strMsj = string.Format("b) --> CANTIDAD registrosSPControlCargaSrv = {0}", registrosSPControlCargaSrv.Count);
      //Console.WriteLine(strMsj);
      //Log.Information(strMsj);

      registrosDataCuadriculaHojaMedidores.OrderBy(r => r.IdDivisionCompra).ThenBy(r => r.EnergeticoId).ThenBy(r => r.IdMedidor).ThenBy(r => r.Anho).ThenBy(r => r.Mes);
      registrosSPControlCargaSrv.OrderBy(r => r.DivisionId).ThenBy(r => r.EnergeticoId).ThenBy(r => r.NroMedidorCombinado);
      //sb.Clear();

      //celRef = new CellReference(configReporteControlCarga.CeldaNombreServicioExcelHojasEdificioMedidor);
      //rowNum = celRef.Row;
      //colNum = celRef.Col;
      //cell = hojaResumenMedidor.GetRow(rowNum).GetCell(colNum);
      //cell.SetCellValue(RegistroServIter.NombreServicio);
      //cell = hojaResumenEdificio.GetRow(rowNum).GetCell(colNum);
      //cell.SetCellValue(RegistroServIter.NombreServicio);
      //celRef = new CellReference(configReporteControlCarga.CeldaNombreServicioExcelHojaResumen);
      //rowNum = celRef.Row;
      //colNum = celRef.Col;
      //cell = hojaResumen.GetRow(rowNum).GetCell(colNum);
      //cell.SetCellValue(RegistroServIter.NombreServicio);

      var propsNumericasExcel = configReporteControlCarga.NOMBRES_COLS_EXCELS_NUMEROS;
      string STR_VAL_OK = configReporteControlCarga.ValorCasillaDivMedidorEnerMesOK;
      string STR_VAL_NO = configReporteControlCarga.ValorCasillaDivMedidorEnerMesNO;
      string STR_VAL_Noap = configReporteControlCarga.ValorCasillaDivMedidorEnerMesNoap;

      //strMsj = string.Format("Energetico\tReportaPMG\tDivision\tRegion\tComuna\tDistribuidora\tNroCliente\tNroMedidor\t");
      //sb.Append(strMsj);
      //for (int cntMes = 0; cntMes < cantMeses; cntMes++)
      //{
      //  dateTimeIter = arrFechasCadaMesAnhoMovil[cntMes];
      //  strMsj = string.Format("{0:MMM}\t", dateTimeIter);
      //  sb.Append(strMsj);
      //}
      //Log.Information(sb.ToString());
      //Console.WriteLine(sb.ToString());
      #region Hoja Resumen por Medidor
      int initialRowNum = configReporteControlCarga.FilaInicialExcelIteracionDatos - 1;
      rowNum = initialRowNum;
      foreach (ReporteControlDeCargaSPAgrupado regSPCtrlCarga in registrosSPControlCargaSrv)
      {
        row = hojaResumenMedidor.GetRow(rowNum);
        var propsExcel = configReporteControlCarga.ColumnasNombresCamposExcel;
        //parte izquierda
        for (int p = 0; p < propsExcel.Length; p++)
        {
          string nombreProp = propsExcel[p];
          //string mensajePrueba = string.Format("propiedad[{0}]", nombreProp);
          //Console.WriteLine(mensajePrueba);
          //Log.Information(mensajePrueba);
          object valor = GeneralHelper.ObtenerValorPropiedad(regSPCtrlCarga, nombreProp);
          cell = row.GetCell(p);
          if (propsNumericasExcel.Contains(nombreProp))
          {
            cell.SetCellValue(Convert.ToDouble(valor));
          }
          else if (valor is bool)
          {
            var boolVal = Convert.ToBoolean(valor);
            cell.SetCellValue(configReporteControlCarga.REPORTA_PMG_BOOL_TO_STR[boolVal]);
          }
          else
          {
            cell.SetCellValue(Convert.ToString(valor));
          }
        }

        //sb.Clear();
        //strMsj = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t"
        //                      , regSPCtrlCarga.Energetico
        //                      , regSPCtrlCarga.ReportaPMG ? "Reporta" : "No_Reporta"
        //                      , regSPCtrlCarga.Division
        //                      , regSPCtrlCarga.RegionId
        //                      , regSPCtrlCarga.Comuna
        //                      , regSPCtrlCarga.EmpresaDistribuidora
        //                      , regSPCtrlCarga.NroCliente
        //                      , regSPCtrlCarga.NroMedidor);
        //sb.Append(strMsj);
        colNum = CellReference.ConvertColStringToIndex(configReporteControlCarga.ColumnaInicialExcelIteracionMeses);
        for (int nroMesIter = 0; nroMesIter < cantMeses; nroMesIter++)
        {
          cell = row.GetCell(nroMesIter + colNum);
          dateTimeIter = arrFechasCadaMesAnhoMovil[nroMesIter];
          var regCasillaDivEnerMedAnhoMesEnumerable = registrosDataCuadriculaHojaMedidores.Where(c => c.IdDivisionCompra == regSPCtrlCarga.DivisionId);
          regCasillaDivEnerMedAnhoMesEnumerable = regCasillaDivEnerMedAnhoMesEnumerable.Where(c => c.EnergeticoId == regSPCtrlCarga.EnergeticoId);
          regCasillaDivEnerMedAnhoMesEnumerable = regCasillaDivEnerMedAnhoMesEnumerable.Where(c => c.IdMedidor == regSPCtrlCarga.IdMedidor);
          //regCasillaDivEnerMedAnhoMesEnumerable = regCasillaDivEnerMedAnhoMesEnumerable.Where(c => (c.IdNumeroDeCliente > 0 ? c.IdMedidor : -1L) == regSPCtrlCarga.IdMedidores);
          if (regCasillaDivEnerMedAnhoMesEnumerable.Count() == 0)
          {
            cell.SetCellValue(ObtenerTextoCasilla(EstadoCasillaValor.NADA));
            cell.CellStyle = ObtenerEstiloCasilla(EstadoCasillaValor.NO);
          }
          else
          {
            regCasillaDivEnerMedAnhoMesEnumerable = regCasillaDivEnerMedAnhoMesEnumerable.Where(c => c.Anho == dateTimeIter.Year);
            regCasillaDivEnerMedAnhoMesEnumerable = regCasillaDivEnerMedAnhoMesEnumerable.Where(c => c.Mes == dateTimeIter.Month);
            var regEncontrado = regCasillaDivEnerMedAnhoMesEnumerable.FirstOrDefault();
            cell.SetCellValue(ObtenerTextoCasilla((EstadoCasillaValor)regEncontrado.EstadoPorcentajeCasilla));
            cell.CellStyle = ObtenerEstiloCasilla((EstadoCasillaValor)regEncontrado.EstadoComprasCasilla);
            //if (regEncontrado.EstadoComprasCasilla == (sbyte)EstadoCasillaValor.NOAP)
            //{
            //  cell.SetCellValue(STR_VAL_Noap);
            //  cell.CellStyle = estiloCeldaPorDefecto;
            //  continue;
            //}
            //switch (regEncontrado.EstadoComprasCasilla)
            //{
            //  cell.CellStyle = estiloCeldaHayComprasConObs;

            //}
            //strMsj = string.Format("<{0,-4}>=<{1}>{2:MM-yyyy}\t", regEncontrado?.ValorStrCelda, regEncontrado == null ? '?' : regEncontrado.EstadoComprasCasilla[0], dateTimeIter);
            //sb.Append(strMsj);
          }
          cell = null;
        }
        cell = row.GetCell(columnaMesesCompletosReportados);
        cell.CellFormula = string.Format(formulaMesesCompletadosReportados, rowNum + 1);
        cell = null;
        cell = row.GetCell(columnaAlMenosDoceMesesCompletosReportados);
        cell.CellFormula = string.Format(formulaAlMenos12MesesCompletados, rowNum + 1, cantMeses);
        cell = null;
        if (rowNum - initialRowNum < cantRegistrosListaIzq - 1) {
          rowNum++;
          row.CopyRowTo(rowNum);
        }
        row = null;
        //Log.Information(sb.ToString());
        //Console.WriteLine(sb.ToString());
      }
      hojaResumenMedidor.ForceFormulaRecalculation = true;
      #endregion

      #region Hoja Resumen por "Edificio" (por Division)
      int rowIni = configReporteControlCarga.FilaInicialExcelIteracionDatos - 1;
      int colIni = CellReference.ConvertColStringToIndex(configReporteControlCarga.ColumnaInicialExcelIteracionDatos);
      int colIter = colIni;
      row = hojaResumenEdificio.GetRow(rowIni);
      var regCasillaDivAnhoMesEnumerable = registrosDataCuadriculaHojaMedidores.GroupBy(e => new { e.IdDivisionCompra, e.Anho, e.Mes, e.EstadoPorcentajeCasilla }).Select(k => new
      {
        Data = k.Key
       ,
        CuentaTodos = k.Count(c => c.EstadoPorcentajeCasilla == (sbyte)EstadoCasillaValor.OK || c.EstadoPorcentajeCasilla == (sbyte)EstadoCasillaValor.NO)
       ,
        CuentaOkeys = k.Count(v => v.EstadoPorcentajeCasilla == (sbyte)EstadoCasillaValor.OK)

      }).ToList();

      var registrosSPControlCargaDivUnicas = registrosSPControlCargaSrv.GroupBy(g => new { g.DivisionId, g.Division, g.ReportaPMG, g.RegionId, g.RegionPos, g.Comuna }).Select(n => new { Elem = n.Key })
        .OrderBy(r => r.Elem.RegionPos).ThenBy(c => c.Elem.Comuna).ThenBy(d => d.Elem.DivisionId).ToList();
      int cantRegistrosUnicosDivs = registrosSPControlCargaDivUnicas.Count;
      int cantUnidadesQueReportan = registrosSPControlCargaDivUnicas.Where(f => f.Elem.ReportaPMG).Count();

      for (int j = 0; j < cantRegistrosUnicosDivs; j++)
      {
        var regSPCtrlCarga = registrosSPControlCargaDivUnicas.ElementAt(j);
        colIter = colIni;
        row = hojaResumenEdificio.GetRow(rowIni+j);
        cell = row.GetCell(colIter);
        cell.SetCellValue(regSPCtrlCarga.Elem.ReportaPMG ? configReporteControlCarga.ValorReportaPMG : configReporteControlCarga.ValorNoReportaPMG);
        colIter++;
        cell = row.GetCell(colIter);
        cell.SetCellValue(regSPCtrlCarga.Elem.Division);
        colIter++;
        cell = row.GetCell(colIter);
        cell.SetCellValue(regSPCtrlCarga.Elem.DivisionId);
        colIter++;
        cell = row.GetCell(colIter);
        cell.SetCellValue(regSPCtrlCarga.Elem.RegionId);
        colIter++;
        cell = row.GetCell(colIter);
        cell.SetCellValue(regSPCtrlCarga.Elem.Comuna);
        colIter++;
        if (!regSPCtrlCarga.Elem.ReportaPMG)
        {
          cell = row.GetCell(colIter);
          cell.SetCellValue(0);
        }
        else
        {
          int cantMesesOK = 0;
          for (int contRangoAnhoMovil = 0; contRangoAnhoMovil < cantMeses; contRangoAnhoMovil++)
          {
            int cantOkeysPorMes = 0;
            int totalTotalCasillasPorMes = 0;
            dateTimeIter = arrFechasCadaMesAnhoMovil[contRangoAnhoMovil];
            var elemFiltradoDivAnhoMes = regCasillaDivAnhoMesEnumerable.Where(r => r.Data.IdDivisionCompra == regSPCtrlCarga.Elem.DivisionId);
            elemFiltradoDivAnhoMes = elemFiltradoDivAnhoMes.Where(d => d.Data.Anho == dateTimeIter.Year);
            elemFiltradoDivAnhoMes = elemFiltradoDivAnhoMes.Where(d => d.Data.Mes == dateTimeIter.Month).ToList();
            var elemDivAnhoMes = elemFiltradoDivAnhoMes.FirstOrDefault();
            if (elemDivAnhoMes == null) continue;
            totalTotalCasillasPorMes = elemDivAnhoMes.CuentaTodos;
            cantOkeysPorMes = elemDivAnhoMes.CuentaOkeys;
            if (totalTotalCasillasPorMes > 0 && cantOkeysPorMes >= totalTotalCasillasPorMes)
            {
              cantMesesOK++;
            }
          }
          cell = row.GetCell(colIter);
          cell.SetCellValue(cantMesesOK);
        }
        if (j < cantRegistrosUnicosDivs - 1)
          row.CopyRowTo(rowIni+j+1);
      }
      hojaResumenEdificio.ForceFormulaRecalculation = true;
      #endregion

      #region Hoja Resumen (completo)
      var conjuntoRegistrosDivUnicasConDataHojaMedidores = from regCuadricula in registrosDataCuadriculaHojaMedidores
                                                           join regSpDivUnica in registrosSPControlCargaDivUnicas on regCuadricula.IdDivisionCompra equals regSpDivUnica.Elem.DivisionId
                                                           where regSpDivUnica.Elem.ReportaPMG
                                                           select new { regSpDivUnica.Elem.DivisionId, regCuadricula.Anho, regCuadricula.Mes, regCuadricula.ValorStrCelda };
      var registrosSPControlCargaDivUnicasQueReportan = conjuntoRegistrosDivUnicasConDataHojaMedidores
        .GroupBy(d => new { d.Anho, d.Mes, d.DivisionId, d.ValorStrCelda })
        .Select(e => new { e.Key.Anho, e.Key.Mes, e.Key.DivisionId
        , CuentaCualquieraTotalAnhoMesDiv = e.Count()
        , CuentaOkTotalAnhoMesDiv = e.Count(c => c.ValorStrCelda == STR_VAL_OK)
        , CuentaNoTotalAnhoMesDiv = e.Count(c => c.ValorStrCelda == STR_VAL_NO)
        });
        //.GroupBy(g => new { ClaveMes = g.Key.Mes, ClaveAnho = g.Key.Anho, CantEdifConInfo = g.Count(e => g.Key.EdifConInfo) }).ToList();
      celRef = new CellReference(configReporteControlCarga.CeldaCantEdificiosQueReportanResumen);
      int rowIterRes = celRef.Row;
      colIter = celRef.Col;
      cell = hojaResumen.GetRow(rowIterRes).GetCell(colIter);
      cell.SetCellValue(cantUnidadesQueReportan);
      rowIterRes = configReporteControlCarga.FilaInicialExcelIteracionResumen - 1;
      colIter = CellReference.ConvertColStringToIndex(configReporteControlCarga.ColumnaInicialExcelIteracionResumen);
      row = hojaResumen.GetRow(rowIterRes);
      for (int contRangoAnhoMovil = 0; contRangoAnhoMovil < cantMeses; contRangoAnhoMovil++)
      {
        int cantDivisionesConInfoAnhoMes = 0;
        dateTimeIter = arrFechasCadaMesAnhoMovil[contRangoAnhoMovil];
        var filtroRegistrosSPControlCargaDivUnicasQueReportanAnhoMes = registrosSPControlCargaDivUnicasQueReportan
          .Where(f => f.Anho == dateTimeIter.Year).Where(f => f.Mes == dateTimeIter.Month);
        foreach (var regSPControlCargaDivUnicaIter in filtroRegistrosSPControlCargaDivUnicasQueReportanAnhoMes)
        {
          if (regSPControlCargaDivUnicaIter.CuentaNoTotalAnhoMesDiv > 0) continue;
          if (regSPControlCargaDivUnicaIter.CuentaOkTotalAnhoMesDiv == regSPControlCargaDivUnicaIter.CuentaCualquieraTotalAnhoMesDiv)
          {
            cantDivisionesConInfoAnhoMes++;
          }
        }
        //var filtroCasillaEnerMedAnhoMesEnumerable = registrosSPControlCargaDivUnicasQueReportan.Where(a => a.Anho == dateTimeIter.Year);
        //filtroCasillaEnerMedAnhoMesEnumerable = registrosSPControlCargaDivUnicasQueReportan.Where(m => m.Mes == dateTimeIter.Month);
        //var casillaEnerMedAnhoResumen = filtroCasillaEnerMedAnhoMesEnumerable.FirstOrDefault();
        //if (casillaEnerMedAnhoResumen != null)
        //{
        //  cantDivisionesConInfoAnhoMes = casillaEnerMedAnhoResumen.
        //}

        cell = row.GetCell(colIter);
        cell.SetCellValue(cantDivisionesConInfoAnhoMes);
        colIter++;
        //var infoCasillasHojaResumen = casillasTotalesDivQueReportanMesAnho.FirstOrDefault();
        //if (infoCasillasHojaResumen != null) {
        //  cantTotalCasillasPorMes = infoCasillasHojaResumen.CantidadTotal;
        //  cantOkeysPorMes = infoCasillasHojaResumen.CantidadOks;

        //}
        //if (cantTotalCasillasPorMes == 0)
        //{
        //  cell.SetCellValue(0);
        //  continue;
        //}
        //cell.SetCellValue(0);
      }
      hojaResumen.ForceFormulaRecalculation = true;
      registrosReporteControlCargaSP.Clear();
      registrosReporteControlCargaSP = null;
      registrosSPControlCargaSrv.Clear();
      registrosSPControlCargaSrv = null;
      //medidorIdListToCambioId.Clear();
      medidorIdListToCambioId = null;
      cambioNroMedidorCompuesto.Clear();
      cambioNroMedidorCompuesto = null;
      #endregion
    }

    private string ObtenerTextoCasilla(EstadoCasillaValor estadoCasillaPorcentaje)
    {
      if (estadoCasillaPorcentaje == EstadoCasillaValor.NADA) return STR_VAL_NO;
      if (estadoCasillaPorcentaje == EstadoCasillaValor.NOAP) return STR_VAL_Noap;
      return estadoCasillaPorcentaje == EstadoCasillaValor.OK ? STR_VAL_OK : STR_VAL_NO;
    }

    private ICellStyle ObtenerEstiloCasilla(EstadoCasillaValor estadoCasillaCompras)
    {
      ICellStyle cellStyleResult = null;
      switch (estadoCasillaCompras)
      {
        case EstadoCasillaValor.CON_OBS:
          cellStyleResult = estiloCeldaHayComprasConObs;
          break;
        case EstadoCasillaValor.SIN_REV:
          cellStyleResult = estiloCeldaHayComprasSinRev;
          break;
        case EstadoCasillaValor.OK:
          cellStyleResult = estiloCeldaTodasComprasObsOk;
          break;
        case EstadoCasillaValor.NADA:
          cellStyleResult = estiloCeldaNada;
          break;
        default:
          cellStyleResult = estiloCeldaPorDefecto;
          break;
      }
      return cellStyleResult;
    }
  }
}

