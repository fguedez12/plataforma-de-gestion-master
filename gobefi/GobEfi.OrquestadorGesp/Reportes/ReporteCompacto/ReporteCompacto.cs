using OrquestadorGesp.ContextEFNetCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using OrquestadorGesp.AppSettingsJson;
using NPOI.XSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using OrquestadorGesp.Helpers;
using Serilog;
using System.Collections.Generic;
using OrquestadorGesp.DTOs;
using NPOI.OpenXml4Net.Exceptions;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace OrquestadorGesp.Reportes
{
  public class ReporteCompacto : ReporteBaseExtendidoComoInsumo<RegistroCompraProrrateada> //ReporteBase
  {
    public List<ReporteCompactoEncabezadoEnt> registrosReporteCompactoSP = new List<ReporteCompactoEncabezadoEnt>();
    //private ICellStyle estiloCeldaProrrateo = null;
    private ConfReporteCompacto configReporteCompacto;
    private int intervaloTiempoReintentoEncontrarAlgunArchivoRepExtSegs = 1;
    private int columnaInicialRecuadro = -1;
    private int cantRegistrosListaIzq = 0;
    private HashSet<string> propsNumericasExcel;
    private IndexedColors[] coloresAnho = null;
    private int anhoInicialRepCompacto = -1;
    private int anhoFinalRepCompacto = -1;
    private List<RecuadroSubtotalReporteCompacto> subtotales;
    private RecuadroSubtotalReporteCompacto recuadroSubtotalReporteCompacto;
    private readonly string NOMBRE_MES_ENERO = DateTimeExtensions.NombreDelMes(1);
    /*
				protected string strCeldaFraseFechaGeneracion;
				protected string strCeldaNombreServicio;
				protected string strMsj = string.Empty;
				protected Dictionary<short, string> nombresEnergeticos = new Dictionary<short, string>();
				protected Dictionary<long, bool> dictServiciosGenerados = new Dictionary<long, bool>();
				protected Dictionary<string, string> columnasRepExtInsumo = new Dictionary<string, string>();
				protected string formatoFechaDisplay;
				protected string formatoFechaGeneracionReporte;
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
    public ReporteCompacto() : base()
    {
      LogicaComunConstructor();
    }

    private void LogicaComunConstructor()
    {
      subtotales = new List<RecuadroSubtotalReporteCompacto>();
    }
    //private readonly string formatoColumnaStr = "Propiedad {0} = [[{1}]]";
    //private readonly string formatoCeldaStr = "Celda {0}{1} es Nula? {2}";

    public string CalcularTipoEdificio(bool medidorExclusivo, bool medidorMixto)
    {
      string tipoDivision = string.Empty;
      var tipoMedidor = configReporteCompacto.TIPO_MEDIDOR[Convert.ToInt32(medidorExclusivo)];
      if (medidorExclusivo)
      {
        tipoDivision = string.Format(configReporteCompacto.TIPO_EDIFICIO[Convert.ToInt32(medidorMixto)]);
      }
      else
      {
        tipoDivision = configReporteCompacto.TIPO_MEDIDOR[0];
      }
      return tipoDivision;
    }

    public override void InicializarEstructurasDatosReporte()
    {
      //todos estos var van a ser herencia de ReporteBaseExtendidoComoInsumo
      subRutaReporteOrigenOrigRepExt = configReporteExtendido.SubRutaPlantillaExcel;
      strCeldaFraseFechaGeneracion = configReporteCompacto.CeldaFraseFechaGeneracion;
      strCeldaNombreServicio = configReporteCompacto.CeldaNombreServicioExcel;
      celRef = new CellReference(strCeldaFraseFechaGeneracion);
      nroColumnaFraseFechaGeneracion = celRef.Col;
      nroFilaFraseFechaGeneracion = celRef.Row;
      formatoFechaDisplay = ConfReporteBase.FORMATO_SOLO_FECHA;
      formatoFechaGeneracionReporte = ConfReporteBase.FORMATO_SOLO_FECHA_CON_GUION;
      intervaloTiempoReintentoEncontrarAlgunArchivoRepExtMseg = configReporteCompacto.IntervaloTiempoCheckeoExistenciaArchivosExcelRepExtSegs;
      cantTiempoMaximoSecsEsperandoAlgunArchivoRepExt = configReporteCompacto.TiempoEsperaExistenciaAlgunArchivoExcelRepExtSegs;
      columnasRepExtInsumo = configReporteCompacto.ColumnasRepExtInsumo;
      fraseGeneracionFechaRegexPattern = ConfReporteCompacto.FRASE_GENERACION_FECHA_REGEX_PATTERN;
    }

    public override void ObtenerConfReporte()
    {
      ConfReporte = CurrConfGlobal.ReporteCompacto;
      configReporteCompacto = (ConfReporteCompacto)ConfReporte;
      configReporteExtendido = CurrConfGlobal.ReporteExtendido;
      intervaloTiempoReintentoEncontrarAlgunArchivoRepExtSegs = configReporteCompacto.IntervaloTiempoCheckeoExistenciaArchivosExcelRepExtSegs;
      intervaloTiempoReintentoEncontrarAlgunArchivoRepExtMseg = intervaloTiempoReintentoEncontrarAlgunArchivoRepExtSegs
        * ConfReporteBase.MILLISEGS_EN_UN_SEG;
      columnaInicialRecuadro = CellReference.ConvertColStringToIndex(configReporteCompacto.ColumnaInicialExcelRecuadroAnho);
      anhoInicialRepCompacto = configReporteCompacto.AnhoInicioRecuadros;
      anhoFinalRepCompacto = fechaHoy.Year;
      cantMeses = calendarHoy.GetMonthsInYear(fechaHoy.Year);
      cantAnhos = anhoFinalRepCompacto - anhoInicialRepCompacto + 1;
      propsNumericasExcel = configReporteCompacto.NOMBRES_COLS_EXCELS_NUMEROS;
      coloresAnho = new IndexedColors[configReporteCompacto.ColoresPorAnho.Length];
      for (int j = 0; j < coloresAnho.Length; j++)
      {
        coloresAnho[j] = IndexedColors.ValueOf(configReporteCompacto.ColoresPorAnho[j]);
      }
    }

    public override void ObtenerDatosSP()
    {
      if (RegistroServIter == null) return;
      //if (EstadoProcesoReporte >= PROCESO_REP_INICIADO) return;
      //if (EstadoProcesoReporte >= PROCESO_REP_EN_PROCESO) return;
      var commandStr = string.Format(PLANTILLA_COD_SQL_EJECUTAR_SP_SIN_PARAMS, string.Format(nombreSP, RegistroServIter.Int64PK));
      var msjCommandStr = string.Format("<{0}>. Comando SQL A Ejecutar=\"{1}\"", GetType().Name, commandStr);
      Console.WriteLine(msjCommandStr);
      Log.Information(msjCommandStr);
      // FALLA:
      // falla obtencion de datos de SP SP_REPORTE_COMPACTO porque esta mapeado una propiedad que no puede ser nula y viene nula
      registrosReporteCompactoSP = DbContextInstance.ReporteCompactoEncabezadoSet.FromSql(commandStr).ToList();
      //EstadoProcesoReporte = PROCESO_REP_INICIADO;
    }

    public override void ObtenerDatosCSV()
    {
    }

    public override void LogicaReporte()
    {
      //List<ReporteCompactoEncabezadoEnt> registrosReporteCompactoIterSrvSP = registrosReporteCompactoSP.ToList();
      XSSFSheet hojaConsumos = (XSSFSheet)PlantillaXltxWb.GetSheetAt(0);
      XSSFSheet hojaCostos = (XSSFSheet)PlantillaXltxWb.GetSheetAt(1);

      strMsj = string.Format(ConfReporteBase.FRASE_STR_FORMAT_BASE_FECHA_GENERACION_REPORTE, DateTime.Now);

      celRef = new CellReference(configReporteCompacto.CeldaFraseFechaGeneracion);
      row = hojaConsumos.GetRow(celRef.Row);
      cell = row.GetCell(celRef.Col);
      cell.SetCellValue(strMsj);
      row = hojaCostos.GetRow(celRef.Row);
      cell = row.GetCell(celRef.Col);
      cell.SetCellValue(strMsj);

      celRef = new CellReference(configReporteCompacto.CeldaNombreServicioExcel);
      row = hojaConsumos.GetRow(celRef.Row);
      cell = row.GetCell(celRef.Col);
      cell.SetCellValue(RegistroServIter.NombreServicio);
      row = hojaCostos.GetRow(celRef.Row);
      cell = row.GetCell(celRef.Col);
      cell.SetCellValue(RegistroServIter.NombreServicio);

      if (registrosReporteCompactoSP.Count == 0)
      {
        //row = hojaConsumos.GetRow(configReporteCompacto.Fi)
        TerminarGeneracionReporte();
      }

      //List<ReporteCompactoEncabezadoEnt> registrosReporteCompactoSP = registrosReporteCompactoSP.ToList();
      //var filasSubtotalesTodosLosEnergDivUnicos = registrosReporteCompactoIterSrvSP.Distinct(GeneralHelper.REPORTE_COMPACTO_ENCABEZADO_ENT_SOLO_DIV_COMPARER).ToList();
      var filasSubtotalesTodosLosEnergDivUnicos = registrosReporteCompactoSP.Distinct(GeneralHelper.REPORTE_COMPACTO_ENCABEZADO_ENT_SOLO_DIV_COMPARER).ToList(); 
      var cantFilasSubtotalesTodosLosEnerDivUnicos = filasSubtotalesTodosLosEnergDivUnicos.Count;
      strMsj = string.Format("{0} => IdSrv={1} ### Cant registrosReporteCompactoSP={2}, cantFilasSubtotalesTodosLosEnerDivUnicos={3} (^o^)/~~~~~~"
        , GetType().Name
        , RegistroServIter.Int64PK
        , registrosReporteCompactoSP.Count
        , cantFilasSubtotalesTodosLosEnerDivUnicos
        );
      Console.WriteLine(strMsj);
      Log.Information(strMsj);

      List<ReporteCompactoEncabezadoEnt> registrosSubTotalTodosEnergFilaIzq = new List<ReporteCompactoEncabezadoEnt>();

      ReporteCompactoEncabezadoEnt repSPSubTotalTodosLosEnergFilaIzq;
      foreach (var elemSubTotalTodosLosEnergFilaIzq in filasSubtotalesTodosLosEnergDivUnicos)
      {
        repSPSubTotalTodosLosEnergFilaIzq = new ReporteCompactoEncabezadoEnt();
        elemSubTotalTodosLosEnergFilaIzq.CopyProperties(repSPSubTotalTodosLosEnergFilaIzq);
        repSPSubTotalTodosLosEnergFilaIzq.EnergeticoId = ConfReporteBase.ID_ENERGETICO_TODOS;
        repSPSubTotalTodosLosEnergFilaIzq.Energetico = ConfReporteBase.NOMBRE_ENERGETICO_TODOS;
        registrosSubTotalTodosEnergFilaIzq.Add(repSPSubTotalTodosLosEnergFilaIzq);
      }
      registrosReporteCompactoSP.AddRange(registrosSubTotalTodosEnergFilaIzq);
      registrosSubTotalTodosEnergFilaIzq.Clear();
      filasSubtotalesTodosLosEnergDivUnicos.Clear();
      // (IList<ReporteCompactoEncabezadoEnt>)
      var listaEnergeticosRepCompacto = registrosReporteCompactoSP.Distinct(GeneralHelper.REPORTE_COMPACTO_ENCABEZADO_ENT_SOLO_ENE_COMPARER).ToList();

      foreach (var itemEnergetico in listaEnergeticosRepCompacto)
      {
        nombresEnergeticos[(short)itemEnergetico.EnergeticoId] = itemEnergetico.Energetico;
      }

      listaEnergeticosRepCompacto = null;
      //nombresEnergeticos[ConfReporteBase.ID_ENERGETICO_TODOS] = ConfReporteBase.NOMBRE_ENERGETICO_TODOS;

      foreach (var itemEnergeticoKey in nombresEnergeticos)
      {
        strMsj = string.Format("{0} => Id Energ: {1,5}, Nombre: {2}", GetType().Name, itemEnergeticoKey.Key, itemEnergeticoKey.Value);
        Console.WriteLine(strMsj);
        Log.Information(strMsj);
      }

      int columnaInicialInsumoRepExt = CellReference.ConvertColStringToIndex(configReporteExtendido.ColumnaInicialExcelIteracionDatos);
      //Thread.Sleep(CurrentConfJson.CurrentConf.TiempoReposoHebrasParaNoAhogarCPUMsec);
      //XSSFWorkbook xssfWorkbookOrigenData = new XSSFWorkbook(fileNameExcelRepExt);
      //List<RegistroCompraProrrateada> conjuntoComprasProrrateadas =
      //  ExcelHelper.ObtenerInfoDesdeInsumoXlsx<RegistroCompraProrrateada>(fileNameExcelRepExt
      //                                        , columnaInicialInsumoRepExt, configReporteExtendido.FilaInicialExcelIteracionDatos - 1
      //                                        , configReporteCompacto.ColumnasRepExtInsumo, 0, configReporteExtendido.ColumnasNombresCamposExcel.Length - 1);
      //Thread.Sleep(CurrentConfJson.CurrentConf.TiempoReposoHebrasParaNoAhogarCPUMsec);
      //conjuntoComprasProrrateadas = conjuntoComprasProrrateadas.Where(tc => tc.TipoTransaccion == configReporteExtendido.ValorTipoTransaccionConsumo).ToList();
      conjuntoComprasProrrateadas.RemoveAll(tc => tc.TipoTransaccion == configReporteExtendido.ValorTipoTransaccionCompra);

      subtotales.Clear();

      foreach (ReporteCompactoEncabezadoEnt costoConsumoRepCompactoIter in registrosReporteCompactoSP)
      {
        for (int anhoCostoConsumo = anhoInicialRepCompacto; anhoCostoConsumo <= anhoFinalRepCompacto; anhoCostoConsumo++)
        {
          for (byte mesCostoConsumo = 1; mesCostoConsumo <= cantMeses; mesCostoConsumo++)
          {
            recuadroSubtotalReporteCompacto = new RecuadroSubtotalReporteCompacto(anhoCostoConsumo, mesCostoConsumo, costoConsumoRepCompactoIter.DivisionId, costoConsumoRepCompactoIter.EnergeticoId, 0.0f, 0.0f);
            subtotales.Add(recuadroSubtotalReporteCompacto);
          }
        }
      }

      int largoSubtotalesEneroAnhoIni = subtotales.Where(f => f.Mes == 1).Where(f => f.Anho == anhoInicialRepCompacto).Count();
      //no debieran haber compras prorrateadas con el energetico artificial "Todfos" (short.MaxValue)
      conjuntoComprasProrrateadas.RemoveAll(e => e.EnergeticoId == ConfReporteBase.ID_ENERGETICO_TODOS);
      conjuntoComprasProrrateadas.RemoveAll(y => y.FinLectura.Year < anhoInicialRepCompacto);

      strMsj = string.Format("{7} => IdSrv={0} ### Cant conjuntoComprasProrrateadas solo consumos={1} ### Nro. registrosReporteCompactoSP={2} ### cant subtotales={3} ### Cant subtotales {4} de {5}={6}"
        , RegistroServIter.Int64PK
        , conjuntoComprasProrrateadas.Count
        , registrosReporteCompactoSP.Count
        , subtotales.Count
        , NOMBRE_MES_ENERO
        , anhoInicialRepCompacto
        , largoSubtotalesEneroAnhoIni
        , GetType().Name
      );
      Console.WriteLine(strMsj);
      Log.Information(strMsj);

      //agrupar por division, energetico, anho y mes de fin de lectura cada compra prorrateada
      List<RegistroCompraProrrateada> grupoCostoConsumoAnhoMesDivEnerg = conjuntoComprasProrrateadas.Distinct().ToList();
      //var pivoteCostoConsumoAnhoMesDivEnerg = grupoCostoConsumoAnhoMesDivEnerg.GroupBy(group =>
      //new { group.IdDivisionCompra, group.EnergeticoId, group.FinLectura.Year, group.FinLectura.Month })
      //.Select( grp => new { Elem = grp.Key, SumaConsumo = grp.Sum(t => t.Cantidad), SumaCosto = grp.Sum(t => t.Costo) });
      var pivoteCostoConsumoAnhoMesDivEnerg = (from grpUnique in grupoCostoConsumoAnhoMesDivEnerg
                                              join com in conjuntoComprasProrrateadas on grpUnique equals com
                                              group com by new RegistroCompraProrrateadaDivEneMesAnho
                                              (
                                                grpUnique.IdDivisionCompra,
                                                grpUnique.EnergeticoId,
                                                grpUnique.FinLectura.Year,
                                                grpUnique.FinLectura.Month
                                              ) into grpSum
                                              select new RegistroCompraProrrateadaAgrupada ( grpSum.Key, grpSum.Sum(g => g.Costo), grpSum.Sum(g => g.Cantidad) )
                                              ).ToList();

      var largoPivote = pivoteCostoConsumoAnhoMesDivEnerg.Count;
      Console.WriteLine("{4} => IdSrv={0} ### cantElems pivoteCostoConsumoAnhoMesDivEnerg={1} ### cantElems registrosReporteCompactoSP={2} ### cant subtotales={3}"
        , RegistroServIter.Int64PK
        , largoPivote
        , registrosReporteCompactoSP.Count
        , subtotales.Count
        , GetType().Name);
      Log.Information("{4} => IdSrv={0} ### cantElems pivoteCostoConsumoAnhoMesDivEnerg={1} ### cantElems registrosReporteCompactoSP={2} ### cant subtotales={3}"
        , RegistroServIter.Int64PK
        , largoPivote
        , registrosReporteCompactoSP.Count
        , subtotales.Count
        , GetType().Name);

      //string plantillaElemConValoresPivotaAnhoMesDivEnerg = "IdSrv={5,4}#itemAgrupado=>IdDivisionCompra={0,5}#EnergeticoId={1,3}#Year={2,4}#Month={3,2}#subtotalNulo={4}";
      foreach (var elemConValores in pivoteCostoConsumoAnhoMesDivEnerg)
      {
        //Thread.Sleep(CurrentConfJson.CurrentConf.TiempoReposoHebrasParaNoAhogarCPUMsec);
        //Console.WriteLine
        var itemAgrupado = elemConValores.Elem;
        var subtotalEncontradoFiltro = subtotales.Where(d => d.DivisionId == itemAgrupado.IdDivisionCompra);
        subtotalEncontradoFiltro = subtotalEncontradoFiltro.Where(e => e.EnergeticoId == itemAgrupado.EnergeticoId);
        subtotalEncontradoFiltro = subtotalEncontradoFiltro.Where(a => a.Anho == itemAgrupado.Year);
        var subtotalEncontrado = subtotalEncontradoFiltro.Where(m => m.Mes == itemAgrupado.Month).FirstOrDefault();

        //Console.WriteLine(plantillaElemConValoresPivotaAnhoMesDivEnerg
        //	, itemAgrupado.IdDivisionCompra
        //	, itemAgrupado.EnergeticoId
        //	, itemAgrupado.Year
        //	, itemAgrupado.Month
        //	, subtotalEncontrado == null ? "S" : "N"
        //	, registroServIter.Int64PK);
        //Log.Information(plantillaElemConValoresPivotaAnhoMesDivEnerg
        //	, itemAgrupado.IdDivisionCompra
        //	, itemAgrupado.EnergeticoId
        //	, itemAgrupado.Year
        //	, itemAgrupado.Month
        //	, subtotalEncontrado == null ? "S" : "N"
        //	, registroServIter.Int64PK);
        //if (subtotalEncontrado != null)
        //{
        strMsj = string.Format("{0} => IdSrv={1}\n ||itemAgrupado[IdDivisionCompra={2}; EnergeticoId={3}; Year={4}; Month={5}]\n" +
          "||subtotalEncontrado[DivisionId={6}; EnergeticoId={7}; Anho={8}; Mes={9}]"
          , GetType().Name
          , RegistroServIter.Int64PK
          , itemAgrupado?.IdDivisionCompra
          , itemAgrupado?.EnergeticoId
          , itemAgrupado?.Year
          , itemAgrupado?.Month
          , subtotalEncontrado?.DivisionId
          , subtotalEncontrado?.EnergeticoId
          , subtotalEncontrado?.Anho
          , subtotalEncontrado?.Mes
        );
        if (subtotalEncontrado == null)
        {
          Console.WriteLine(strMsj);
          Log.Warning(strMsj);
        }
        if (elemConValores == null)
        {
          Console.WriteLine(strMsj);
          Log.Warning(strMsj);
        }
        subtotalEncontrado.Costo = elemConValores.SumaCosto;
        subtotalEncontrado.Consumo = elemConValores.SumaConsumo;
        //} 
      }

      var pivoteSubtotalesDivMesAnhoTodosEner = (from elemPivote in pivoteCostoConsumoAnhoMesDivEnerg
                                                group elemPivote by new RegistroCompraProrrateadaDivMesAnho
                                                {
                                                  IdDivisionCompra = elemPivote.Elem.IdDivisionCompra
                                                  ,
                                                  Month = elemPivote.Elem.Month
                                                  ,
                                                  Year = elemPivote.Elem.Year
                                                } into grpSumDivAnhoMes
                                                select new RegistroCompraProrrateadaDivMesAnhoCostoConsumo
                                                {
                                                  Elem = grpSumDivAnhoMes.Key
                                                  ,
                                                  SumaCostoDivMesAnho = grpSumDivAnhoMes.Sum(gSum => gSum.SumaCosto)
                                                  ,
                                                  SumaConsumoDivMesAnho = grpSumDivAnhoMes.Sum(gSum => gSum.SumaConsumo)
                                                }).ToList();
      //= pivoteCostoConsumoAnhoMesDivEnerg.GroupBy(grpDivMesAnho => new { grpDivMesAnho.Elem.IdDivisionCompra, grpDivMesAnho.Elem.Month, grpDivMesAnho.Elem.Year })
      //.Select(grpDivMesAnhoTodosEnerg => new { Elem = grpDivMesAnhoTodosEnerg.Key, SumaConsumoDivMesAnho = grpDivMesAnhoTodosEnerg.Sum(st => st.SumaConsumo), SumaCostoDivMesAnho = grpDivMesAnhoTodosEnerg.Sum(st => st.SumaCosto) });
      foreach (var elemConValoresSubTotal in pivoteSubtotalesDivMesAnhoTodosEner)
      {
        //Thread.Sleep(CurrentConfJson.CurrentConf.TiempoReposoHebrasParaNoAhogarCPUMsec);
        var itemAgrupadoSubtotal = elemConValoresSubTotal.Elem;
        var recuadroSubtotalFitroTodosEner = subtotales.Where(s => s.DivisionId == itemAgrupadoSubtotal.IdDivisionCompra);
        recuadroSubtotalFitroTodosEner = recuadroSubtotalFitroTodosEner.Where(e => e.EnergeticoId == ConfReporteBase.ID_ENERGETICO_TODOS);
        recuadroSubtotalFitroTodosEner = recuadroSubtotalFitroTodosEner.Where(a => a.Anho == itemAgrupadoSubtotal.Year);
        var recuadroSubtotalTodosEner = recuadroSubtotalFitroTodosEner.Where(m => m.Mes == itemAgrupadoSubtotal.Month).FirstOrDefault();
        //Console.WriteLine(plantillaElemConValoresPivotaAnhoMesDivEnerg
        //	, itemAgrupadoSubtotal.IdDivisionCompra
        //	, ConfReporteBase.ID_ENERGETICO_TODOS
        //	, itemAgrupadoSubtotal.Year
        //	, itemAgrupadoSubtotal.Month
        //	, recuadroSubtotalTodosEner == null ? "S" : "N"
        //	, registroServIter.Int64PK);
        //Log.Information(plantillaElemConValoresPivotaAnhoMesDivEnerg
        //	, itemAgrupadoSubtotal.IdDivisionCompra
        //	, ConfReporteBase.ID_ENERGETICO_TODOS
        //	, itemAgrupadoSubtotal.Year
        //	, itemAgrupadoSubtotal.Month
        //	, recuadroSubtotalTodosEner == null ? "S" : "N"
        //	, registroServIter.Int64PK);

        recuadroSubtotalTodosEner.Costo = Convert.ToSingle(elemConValoresSubTotal?.SumaCostoDivMesAnho);
        recuadroSubtotalTodosEner.Consumo = Convert.ToSingle(elemConValoresSubTotal?.SumaConsumoDivMesAnho);
      }

      //var pivoteTotalesDivEnerAnho = subtotales.GroupBy(g => new { g.DivisionId, g.Anho, g.EnergeticoId })
      //	.Select(group => new { Elem = group.Key, SumaConsumoDivEnergAnho = group.Sum(st => st.Consumo), SumaCostoDivEnergAnho = group.Sum(st => st.Costo) });

      //foreach (var elemConValoresTotal in pivoteTotalesDivEnerAnho)
      //{
      //	//Thread.Sleep(CurrentConfJson.CurrentConf.TiempoReposoHebrasParaNoAhogarCPUMsec);
      //	var itemAgrupadoTotal = elemConValoresTotal.Elem;
      //	recuadroTotalReporteCompacto = new RecuadroTotalReporteCompacto(
      //		itemAgrupadoTotal.Anho
      //		, itemAgrupadoTotal.DivisionId
      //		, itemAgrupadoTotal.EnergeticoId
      //		, elemConValoresTotal.SumaConsumoDivEnergAnho
      //		, elemConValoresTotal.SumaCostoDivEnergAnho);
      //	totales.Add(recuadroTotalReporteCompacto);
      //}

      var listaIzquierda = (from reg in registrosReporteCompactoSP
                           select new RegistroReporteCompactoListIzq
                           {
                             IdDivision = reg.DivisionId,
                             EnergeticoId = reg.EnergeticoId,
                             Division = reg.Division,
                             Energetico = reg.Energetico,
                             MedidorExclusivo = reg.MedidorExclusivo,
                             MedidorMixto = reg.MedidorMixto,
                             Superficie = reg.Superficie,
                             ClasificacionEdificio = reg.ClasificacionEdificio,
                             TipoEdificio = CalcularTipoEdificio(reg.MedidorExclusivo, reg.MedidorMixto),
                             RegionNom = reg.RegionNom,
                             RegionPos = reg.RegionPos,
                             Comuna = reg.Comuna
                           }).OrderBy(r => r.RegionPos).ThenBy(c => c.Comuna).ThenBy(d => d.IdDivision).ThenBy(e => e.EnergeticoId).ToList();


      //listaIzquierda = listaIzquierda.OrderBy(r => r.RegionPos).ThenBy(c => c.Comuna).ThenBy(d => d.IdDivision).ThenBy(e => e.EnergeticoId).ToList();

      string tipoDivision = string.Empty;
      string tipoMedidor = string.Empty;

      cantRegistrosListaIzq = listaIzquierda.Count();

      int colOrigenTotal = columnaInicialRecuadro + cantMeses;
      List<XSSFSheet> hojasPlantilla = new List<XSSFSheet>()
              {
                hojaConsumos,
                hojaCostos
              };

      var formulaEvaluator = PlantillaXltxWb.GetCreationHelper().CreateFormulaEvaluator();
      ICellStyle estiloSoloBordeSuperior = PlantillaXltxWb.GetStylesSource().CreateCellStyle();
      ICellStyle[,,] estilosLibroMeses = new ICellStyle[ConfReporteCompacto.LARGO_PRIMERA_DIMENSION_ARR_ESTILOS, cantAnhos, cantMeses];
      ICellStyle[,] estilosLibroAnhos = new ICellStyle[ConfReporteCompacto.LARGO_PRIMERA_DIMENSION_ARR_ESTILOS, cantAnhos];
      ICellStyle cellStyle;
      estiloSoloBordeSuperior.BorderBottom = BorderStyle.None;
      estiloSoloBordeSuperior.BorderLeft = BorderStyle.None;
      estiloSoloBordeSuperior.BorderRight = BorderStyle.None;
      estiloSoloBordeSuperior.BorderTop = BorderStyle.Medium;
      for (int i = 0; i < ConfReporteCompacto.LARGO_PRIMERA_DIMENSION_ARR_ESTILOS; i++)
      {
        row = hojaConsumos.GetRow(nroFilaInicial - ConfReporteCompacto.LARGO_PRIMERA_DIMENSION_ARR_ESTILOS + i);
        for (int j = 0; j < cantAnhos; j++)
        {
          cell = row.GetCell(columnaInicialRecuadro + cantMeses);
          cellStyle = new XSSFCellStyle(PlantillaXltxWb.GetStylesSource());
          cellStyle.CloneStyleFrom(cell.CellStyle);
          cellStyle.FillForegroundColor = coloresAnho[j].Index;
          cellStyle.FillPattern = FillPattern.SolidForeground;
          estilosLibroAnhos[i, j] = cellStyle;
          for (int k = 0; k < cantMeses; k++)
          {
            cell = row.GetCell(columnaInicialRecuadro + k);
            cellStyle = new XSSFCellStyle(PlantillaXltxWb.GetStylesSource());
            cellStyle.CloneStyleFrom(cell.CellStyle);
            cellStyle.FillForegroundColor = coloresAnho[j].Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            estilosLibroMeses[i, j, k] = cellStyle;
          }
        }
      }
      //Thread.Sleep(CurrentConfJson.CurrentConf.TiempoReposoHebrasParaNoAhogarCPUMsec);

      foreach (XSSFSheet hojaIter in hojasPlantilla)
      {
        int colDestino = columnaInicialRecuadro + cantAnhos * cantMeses;
        int colOrigenSuma = -1;
        string colInicioSumaStr = null;
        string colFinSumaStr = null;
        //rellenando frase de fecha de generacion para hojas de Consumo Energético y Gasto Energético

        // Loop para rellenar Primero Molde Subtotales por anho (anhoInicialRepCompacto hasta anho de fecha de ejecucion Orquestador)
        for (int anhoSubTotalIndex = 0; anhoSubTotalIndex < cantAnhos; anhoSubTotalIndex++)
        {
          //Thread.Sleep(CurrentConfJson.CurrentConf.TiempoReposoHebrasParaNoAhogarCPUMsec);
          colOrigenSuma = columnaInicialRecuadro + anhoSubTotalIndex * cantMeses;
          colInicioSumaStr = CellReference.ConvertNumToColString(colOrigenSuma);
          colFinSumaStr = CellReference.ConvertNumToColString(colOrigenSuma + cantMeses - 1);
          ExcelHelper.CopiarRangoColumnas(hojaIter, colOrigenTotal, colDestino + anhoSubTotalIndex, 1);
          cell = hojaIter.GetRow(nroFilaInicial).GetCell(colDestino + anhoSubTotalIndex);
          string cellFormula = string.Format(ConfReporteCompacto.MOLDE_STRING_FORMULA_SUMA
            , colInicioSumaStr
            , nroFilaInicial + 1
            , colFinSumaStr).Trim();
          cell.SetCellFormula(cellFormula);
          cell = hojaIter.GetRow(nroFilaInicial - ConfReporteCompacto.LARGO_PRIMERA_DIMENSION_ARR_ESTILOS).GetCell(colDestino + anhoSubTotalIndex);
          cell.SetCellValue(string.Format(ConfReporteCompacto.PLANTILLA_TOTAL_ANHO_TITULO, anhoInicialRepCompacto + anhoSubTotalIndex));
          cell.CellStyle = estilosLibroAnhos[0, anhoSubTotalIndex];
          cell = hojaIter.GetRow(nroFilaInicial - ConfReporteCompacto.LARGO_PRIMERA_DIMENSION_ARR_ESTILOS + 1).GetCell(colDestino + anhoSubTotalIndex);
          cell.CellStyle = estilosLibroAnhos[1, anhoSubTotalIndex];
        }
        //Thread.Sleep(CurrentConfJson.CurrentConf.TiempoReposoHebrasParaNoAhogarCPUMsec);

        // Loop para rellenar celdas de enero=1 a diciembre=12 para cada "Iteracion" (cantidad de anhos desde el anhoInicialRepCompacto 
        // inicial el anho de ejecucion del Orquestador)
        for (int anhoTotalIndex = 0; anhoTotalIndex < cantAnhos; anhoTotalIndex++)
        {
          colDestino = columnaInicialRecuadro + cantMeses * anhoTotalIndex;
          if (anhoTotalIndex > 0)
          {
            ExcelHelper.CopiarRangoColumnas(hojaIter, columnaInicialRecuadro, colDestino, cantMeses);
            cell = hojaIter.GetRow(nroFilaInicial - 3).GetCell(colDestino);
            cell.SetCellValue(string.Empty);
          }
          cell = hojaIter.GetRow(nroFilaInicial - 2).GetCell(colDestino);
          cell.SetCellValue(anhoInicialRepCompacto + anhoTotalIndex);
          for (int mesTotalIndex = 0; mesTotalIndex < cantMeses; mesTotalIndex++)
          {
            cell = hojaIter.GetRow(nroFilaInicial - ConfReporteCompacto.LARGO_PRIMERA_DIMENSION_ARR_ESTILOS).GetCell(colDestino + mesTotalIndex);
            cell.CellStyle = estilosLibroMeses[0, anhoTotalIndex, mesTotalIndex];
            cell = hojaIter.GetRow(nroFilaInicial - ConfReporteCompacto.LARGO_PRIMERA_DIMENSION_ARR_ESTILOS + 1).GetCell(colDestino + mesTotalIndex);
            cell.CellStyle = estilosLibroMeses[1, anhoTotalIndex, mesTotalIndex];
          }
        }
        //Thread.Sleep(CurrentConfJson.CurrentConf.TiempoReposoHebrasParaNoAhogarCPUMsec);
      }

      foreach (ISheet hojaIter in hojasPlantilla)
      {
        int nroFilaIter = nroFilaInicial;
        foreach (var regListaIzq in listaIzquierda)
        {
          row = hojaIter.GetRow(nroFilaIter);
          var propsExcel = configReporteCompacto.ColumnasNombresCamposExcel;
          //parte izquierda
          for (int p = 0; p < propsExcel.Length; p++)
          {
            string nombreProp = propsExcel[p];
            //string mensajePrueba = string.Format("propiedad[{0}]", nombreProp);
            //Console.WriteLine(mensajePrueba);
            //Log.Information(mensajePrueba);
            object valor = GeneralHelper.ObtenerValorPropiedad(regListaIzq, nombreProp);
            cell = row.GetCell(p);
            if (propsNumericasExcel.Contains(nombreProp))
            {
              cell.SetCellValue(Convert.ToDouble(valor));
            }
            else
            {
              cell.SetCellValue(Convert.ToString(valor));
            }
          }

          //valores recuadros meses para cada anho, energetico-division
          for (int anhoRecuadroIter = anhoInicialRepCompacto; anhoRecuadroIter <= anhoFinalRepCompacto; anhoRecuadroIter++)
          {
            for (byte mesRecuadroIter = 1; mesRecuadroIter <= cantMeses; mesRecuadroIter++)
            {
              //Thread.Sleep(CurrentConfJson.CurrentConf.TiempoReposoHebrasParaNoAhogarCPUMsec);
              int nroColumnaRecuadroIter = columnaInicialRecuadro + (anhoRecuadroIter - anhoInicialRepCompacto) * cantMeses + (mesRecuadroIter - 1);
              var subtotalAnhoMesDivEnerg = subtotales
                .Where(d => d.DivisionId == regListaIzq.IdDivision)
                .Where(e => e.EnergeticoId == regListaIzq.EnergeticoId)
                .Where(a => a.Anho == anhoRecuadroIter)
                .Where(m => m.Mes == mesRecuadroIter).FirstOrDefault();

              var consumoMesAnhoEneDiv = 0.0d;
              var costoMesAnhoEneDiv = 0.0d;
              if (subtotalAnhoMesDivEnerg != null)
              {
                consumoMesAnhoEneDiv = subtotalAnhoMesDivEnerg.Consumo;
                costoMesAnhoEneDiv = subtotalAnhoMesDivEnerg.Costo;
              }
              cell = row.GetCell(nroColumnaRecuadroIter);
              if (hojaIter == hojaConsumos)
              {
                cell.SetCellValue(consumoMesAnhoEneDiv);
              }
              else
              {
                cell.SetCellValue(costoMesAnhoEneDiv);
              }
            }
          }

          //valores columnas totales anho para cada energetico-division (se probará si funciona reevaluando la formula del total
          for (int anhoTotalesIter = anhoInicialRepCompacto; anhoTotalesIter <= anhoFinalRepCompacto; anhoTotalesIter++)
          {
            //Thread.Sleep(CurrentConfJson.CurrentConf.TiempoReposoHebrasParaNoAhogarCPUMsec);
            int nroColumnaTotalesIter = columnaInicialRecuadro + cantAnhos * cantMeses + (anhoTotalesIter - anhoInicialRepCompacto);
            ICell cellTotal = row.GetCell(nroColumnaTotalesIter);
            string cellFormula = cellTotal.CellFormula;
            Match match = Regex.Match(cellTotal.CellFormula, ConfReporteCompacto.PATRON_REGEX_FORMULA_SUMA);
            if (match != null && match.Groups.Count > 2)
            {
              string colInicioRango = match.Groups[1].Value;
              string colFinRango = match.Groups[2].Value;
              cellTotal.SetCellFormula(string.Format(ConfReporteCompacto.MOLDE_STRING_FORMULA_SUMA, colInicioRango, nroFilaIter + 1, colFinRango));
            }
            formulaEvaluator.EvaluateFormulaCell(cellTotal);
            if (anhoTotalesIter == anhoFinalRepCompacto)
            {
              nroFilaIter++;
              row.CopyRowTo(nroFilaIter);
            }
          }
        }
        int nroFilaUltimo = row.RowNum + 1;
        foreach (var hoja in hojasPlantilla)
        {
          row = hoja.GetRow(nroFilaUltimo);
          if (row != null)
          {
            for (int i = 0; i < columnaInicialRecuadro + cantAnhos * (cantMeses + 1); i++)
            {
              cell = row.GetCell(i);
              if (cell != null)
              {
                cell.SetCellValue(string.Empty);
                cell.SetCellType(CellType.Blank);
                cell.CellStyle = estiloSoloBordeSuperior;
              }
              //Thread.Sleep(CurrentConfJson.CurrentConf.TiempoReposoHebrasParaNoAhogarCPUMsec);
            }
          }
        }
      }
      registrosSubTotalTodosEnergFilaIzq.Clear();
      registrosReporteCompactoSP.Clear();
      filasSubtotalesTodosLosEnergDivUnicos.Clear();
      //listaEnergeticosRepCompacto.Clear();
      //nombresEnergeticos.Clear();
      conjuntoComprasProrrateadas.Clear();
      grupoCostoConsumoAnhoMesDivEnerg.Clear();
      pivoteCostoConsumoAnhoMesDivEnerg.Clear();
      pivoteSubtotalesDivMesAnhoTodosEner.Clear();
      listaIzquierda.Clear();
      //hojasPlantilla.Clear();

      //registrosSubTotalTodosEnergFilaIzq = null;
      //registrosReporteCompactoSP = null;
      //filasSubtotalesTodosLosEnergDivUnicos = null;
      //listaEnergeticosRepCompacto = null;
      //nombresEnergeticos = null;
      //conjuntoComprasProrrateadas = null;
      //grupoCostoConsumoAnhoMesDivEnerg = null;
      //pivoteCostoConsumoAnhoMesDivEnerg = null;
      //pivoteSubtotalesDivMesAnhoTodosEner = null;
      //listaIzquierda = null;
      //hojasPlantilla = null;
      //estiloSoloBordeSuperior = null;
      //estilosLibroMeses = null;
      //estilosLibroAnhos = null;
      //estiloSoloBordeSuperior = null;
    }
  }
}