using NPOI.OpenXml4Net.Exceptions;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using OrquestadorGesp.AppSettingsJson;
using OrquestadorGesp.ContextEFNetCore;
using OrquestadorGesp.DTOs;
using OrquestadorGesp.Helpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace OrquestadorGesp.Reportes
{
  public abstract class ReporteBaseExtendidoComoInsumo<DTO> : ReporteBase
    where DTO : BaseDTO, new()
  {
    private Dictionary<long, bool> dictServiciosGenerados = new Dictionary<long, bool>();
    private HashSet<long> setServiciosEsperando = new HashSet<long>();
    protected Calendar calendarHoy = new GregorianCalendar();
    protected int cantAnhos = -1;
    protected int cantMeses = -1;
    public const int PROCESO_REP_INICIADO = 1;
    public const int PROCESO_REP_EN_PROCESO = 2;
    public const int PROCESO_REP_TERMINADO = 4;
    public const int PROCESO_REP_YA_ESTA_ACTUALIZADO = 8;
    protected DateTime fechaHoy = DateTime.Now.Date;
    protected CellReference celRef = null;
    protected ICell cell = null;
    protected IRow row = null;
    //private new _BasePkServicioEntity registroServIter;
    #region Variables protegidas que solo la clase que IMPLEMENTA sabe como llenarlas
    protected string strCeldaFraseFechaGeneracion;
    protected string strCeldaNombreServicio;
    protected string strMsj = string.Empty;
    protected Dictionary<short, string> nombresEnergeticos = new Dictionary<short, string>();
    protected Dictionary<string, string> columnasRepExtInsumo = new Dictionary<string, string>();
    private Dictionary<long, bool> repExtendidoExiste = new Dictionary<long, bool>();
    protected string formatoFechaDisplay;
    protected string formatoFechaGeneracionReporte;
    protected string strComentarioFechaMod;
    protected int nroColumnaFraseFechaGeneracion = -1;
    protected int nroFilaFraseFechaGeneracion = -1;
    protected string columnaInicialIteracion;
    protected string subRutaReporteOrigenOrigRepExt;
    protected string rutaPlantillasReportes;
    protected string rutaRaizOrigenRepExt;
    protected string fileNameExcelRepExt;
    protected int cantTiempoMaximoSecsEsperandoAlgunArchivoRepExt = -1;
    protected int tiempoTranscurridoEsperandoAlgunArchivoRepExt = -1;
    protected int intervaloTiempoReintentoEncontrarAlgunArchivoRepExtMseg = -1;
    protected string fraseGeneracionFechaRegexPattern;
    protected ConfReporteExtendido configReporteExtendido;
    protected ConfReporteConInsumoRepExt confReporteConInsumoRepExt;
    protected List<DTO> conjuntoComprasProrrateadas;
    protected _BasePkServicioEntity entityPkServicioBaseIter = null;
    protected List<_BasePkServicioEntity> serviciosInfoBaseList = new List<_BasePkServicioEntity>();
    public static string NOMBRE_CLASE_REP_EXT_INSUMO { get; } = typeof(ReporteBaseExtendidoComoInsumo<DTO>).Name;

    public int EstadoProcesoReporte { get; private set; }
    protected int CantidadExcelsGenerados { get ; private set; }
    public _BasePkServicioEntity RegistroServIter { get; private set; }
    #endregion

    protected ReporteBaseExtendidoComoInsumo() : base()
    {
      //dbContextInstance = GespDbContext.GetInstance();
      cantMeses = calendarHoy.GetMonthsInYear(fechaHoy.Year);
      ObtenerConfReporte();
      nombreSP = ConfReporte.NombreProcedimientoAlmacenado;
      nombreCSV = ConfReporte.RutaCompletaCSV;
      nroFilaInicial = ConfReporte.FilaInicialExcelIteracionDatos - 1;
      CantidadExcelsGenerados = -1;
    }

    public sealed override void TerminarGeneracionReporte()
    {
      setServiciosEsperando.Remove(RegistroServIter.Int64PK);
      dictServiciosGenerados[RegistroServIter.Int64PK] = true;
    }
    /**
		 * Se oculta el constructor ReporteBaseExtendidoComoInsumo(ConfJson confJson)
		 * (En java esto NUNCA se podria hacer pues sólo se puede AUMENTAR o
		 * DEJAR IGUAL la visibilidad de Constructores y Metodos)
		 * */
    private ReporteBaseExtendidoComoInsumo(ConfJson confJson)
    {

    }
    /*
		 * Se sobreescribe para que los reporte que se invoquen por cada Id Servicio 
		 * y que tengan como insumo el reporte extendido 
		 * */
    protected new void LlenarIdsServicios()
    {
      //if (serviciosInfoBaseList.Count == 0)
      //{
      base.LlenarIdsServicios();
      //  serviciosInfoBaseList.AddRange(ListaServiciosBase);
      //}
    }

    public abstract override void ObtenerDatosSP();
    public abstract void InicializarEstructurasDatosReporte();

    public abstract void LogicaReporte();

    public sealed override void ObtenerReporte()
    {
      LlenarIdsServicios();

      //List<RecuadroTotalReporteCompacto> totales = new List<RecuadroTotalReporteCompacto>();
      //List<RecuadroSubtotalReporteCompacto> subtotales = new List<RecuadroSubtotalReporteCompacto>();
      //RecuadroSubtotalReporteCompacto recuadroSubtotalReporteCompacto = null;
      //strCeldaFraseFechaGeneracion = configReporteCompacto.CeldaFraseFechaGeneracion;
      //strCeldaNombreServicio = configReporteCompacto.CeldaNombreServicioExcel;
      InicializarEstructurasDatosReporte();

      configReporteExtendido = CurrConfGlobal.ReporteExtendido;
      try
      {
        string rutaCarpetaRaizOrigen = null;
        string nombrePropClaseConfReporteActual = GetType().Name;
        string dobleBackslash = string.Format("{0}{0}", Path.DirectorySeparatorChar);
        string dosPuntosSlashLetraUnidad = string.Format(":{0}", Path.DirectorySeparatorChar);
        int inicioIndiceStr = 0;
        confReporteConInsumoRepExt = ObjectExtensions.GetPropValue<ConfReporteConInsumoRepExt>(CurrConfGlobal, nombrePropClaseConfReporteActual);
        rutaRaizOrigenRepExt = confReporteConInsumoRepExt.RutaRaizFisicaCarpetaDeRedUnidadRedEtcInsumoRepExt;
        var baseDir = AppContext.BaseDirectory + Path.DirectorySeparatorChar;
        var rutaPlantillasReportes = CurrConfGlobal.RutaPlantillasExcel;
        var subRutaPlantillaReporte = ConfReporte.SubRutaPlantillaExcel;

        //RUTA que seria la por defecto para ORIGEN DEL REPORTE EXTENDIDO cRutaRaizFisicaCarpetaDeRedUnidadRedEtcInsumoRepExtomo INSUMO
        //si la configuracion json "RutaRaizFisicaCarpetaDeRedUnidadRedEtcInsumoRepExt" viene vacía
        //(y se evalua si esta ruta es absoluta o relativa más abajo
        subRutaReporteOrigenOrigRepExt = configReporteExtendido.SubRutaPlantillaExcel; // (*) => valor por defecto segun lo explicado arriba

        //Ruta donde se escribe reporte (DESTINO)
        if (!FileSystemHelper.RutaEsAbsoluta(rutaPlantillasReportes))
        {
          rutaPlantillasReportes = string.Format("{0}{1}", baseDir, rutaPlantillasReportes);
          rutaPlantillasReportes = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(rutaPlantillasReportes);
        }

        #region Procesamiento RUTA ORIGEN DEL REPORTE EXTENDIDO como INSUMO
        //Si la constante de configuracion para origen del reporte extendido "RutaRaizFisicaCarpetaDeRedUnidadRedEtcInsumoRepExt" viene vacía
        //se toma el valor que tenia la variable subRutaPlantillaReporte (punto (*))
        if (string.IsNullOrEmpty(rutaRaizOrigenRepExt))
        {
          rutaCarpetaRaizOrigen = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(string.Format("{0}{1}", baseDir, subRutaReporteOrigenOrigRepExt));
        }
        //Si la constante de configuracion para origen del reporte extendido "RutaRaizFisicaCarpetaDeRedUnidadRedEtcInsumoRepExt" es ruta absoluta
        //se toma como base para obtener los reportes desde ahi (con nombre "${IdServicio}.xlsx")
        else if (FileSystemHelper.RutaEsAbsoluta(rutaRaizOrigenRepExt))
        {
          int posPrimerDobleBacklash = rutaRaizOrigenRepExt.IndexOf(dobleBackslash);
          int posDosPuntosSlashLetraUnidad = rutaRaizOrigenRepExt.IndexOf(dosPuntosSlashLetraUnidad);
          if (posPrimerDobleBacklash == 0) inicioIndiceStr = posPrimerDobleBacklash + 2;
          else if (posDosPuntosSlashLetraUnidad > 0) inicioIndiceStr = posDosPuntosSlashLetraUnidad + 2;

          rutaCarpetaRaizOrigen = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(string.Format("{0}{1}"
          , rutaRaizOrigenRepExt, Path.DirectorySeparatorChar), inicioIndiceStr);
        }
        //Si la constante de configuracion para origen del reporte extendido "RutaRaizFisicaCarpetaDeRedUnidadRedEtcInsumoRepExt" es ruta relativa
        //(por ejemplo "CarpetaReporteExtendido\ReportesExtRevisados") se toma como base la ruta del ejecutable 
        //mas dicha ruta para obtener los reportes desde ahi (ejm  ${ruta Ejecutable} + "\CarpetaReporteExtendido\ReportesExtRevisados\${IdServicio}.xlsx")
        else
        {
          rutaCarpetaRaizOrigen = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(string.Format("{0}{1}{2}{1}"
          , baseDir, Path.DirectorySeparatorChar, rutaRaizOrigenRepExt));
        }

        strMsj = string.Format("<{0}:{1}> => rutaCarpetaRaizOrigen={2}", NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, rutaCarpetaRaizOrigen);
        Console.WriteLine(strMsj);
        Log.Information(strMsj);
        #endregion

        subRutaPlantillaReporte = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(subRutaPlantillaReporte);
        if (FileSystemHelper.RutaEsAbsoluta(subRutaPlantillaReporte))
        {
          Log.Warning("<{0}:{1}> La configuracion \"SubRutaPlantillaExcel\" NO PUEDE SER ABSOLUTA. Valor en archivo de configuracion json:{2}"
            , NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, subRutaPlantillaReporte);
          Log.Warning("Saliendo de {0}:{1}.obtenerReporte()", NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name);
          return;
        }

        //if (FileSystemHelper.RutaEsAbsoluta(subRutaReporteOrigen))
        //{
        //	Log.Warning("La configuracion \"SubRutaPlantillaExcel\" para configuracion \"{0}\" que es utlizada por \"{1}\"" +
        //		" NO PUEDE SER ABSOLUTA. Valor en archivo de configuracion json:{2}\n", typeof(ConfReporteExtendido).Name, GetType().Name, subRutaPlantillaReporte);
        //	Log.Warning("Saliendo de {0}.obtenerReporte()", GetType().Name);
        //	return;
        //}

        #region Variables Configuracion Reporte
        string archivoPlantillaReporte = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(ConfReporte.ArchivoPlantillaExcel);
        string rutaPlantillaReporte = rutaPlantillasReportes.EndsWith(FileSystemHelper.DIR_SEPARATOR) ?
          GeneralHelper.RemoveLastChar(rutaPlantillasReportes) : rutaPlantillasReportes;
        string subRutaPlantillaReporteExtPorServ = subRutaPlantillaReporte.EndsWith(FileSystemHelper.DIR_SEPARATOR) ?
          GeneralHelper.RemoveLastChar(subRutaPlantillaReporte) : subRutaPlantillaReporte;
        rutaPlantillaReporte = String.Format("{0}{1}{2}{1}{3}"
          , rutaPlantillaReporte
          , FileSystemHelper.DIR_SEPARATOR
          , subRutaPlantillaReporte
          , archivoPlantillaReporte);
        ////var REPORTE_CONF_COMPACTO = (ConfReporteCompacto)ConfReporte;
        columnaInicialIteracion = ConfReporte.ColumnaInicialExcelIteracionDatos;
        formatoFechaDisplay = CurrConfGlobal.FormatoFechaDisplay;
        formatoFechaGeneracionReporte = ConfReporteBase.FORMATO_SOLO_FECHA_CON_GUION;
        string extensionArchivoReporte = rutaPlantillaReporte.Substring(
          rutaPlantillaReporte.LastIndexOf('.') + 1
        );

        string rutaCarpetaRaizDestino = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(string.Format("{0}{1}", baseDir, subRutaPlantillaReporte));
        Directory.CreateDirectory(rutaCarpetaRaizDestino);
        #endregion
        using (FileStream fileStream = new FileStream(rutaPlantillaReporte, FileMode.Open, FileAccess.Read))
        {
          PlantillaXltxWb = new XSSFWorkbook(fileStream);
          fileStream.Close();
        }
        // averiguar otra forma de agrupar ids y nombres servicios pues arroja solamente una ocurrencia ID: 3 ; Ejercito de Chile
        var columnaInicial = columnaInicialIteracion[0];
        if (columnaInicial > 'Z')
        {
          Log.Warning("<{0}:{1}> La columna inicial debe estar entre A y Z. Valor de \"ColumnaInicialIteracionDatosSPUnidadesPorServicio\" en archivo de configuracion json excede limite superior:{2}"
            , NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, columnaInicial);
          Log.Warning("Saliendo de {0}:{1}.obtenerReporte()", NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name);
          return;
        }

        if (columnaInicial < 'A')
        {
          Log.Warning("La columna inicial debe estar entre A y Z. Valor de \"ColumnaInicialIteracionDatosSPUnidadesPorServicio\" en archivo de configuracion json excede limite inferior:{2}"
            , NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, columnaInicial);
          Log.Warning("Saliendo de {0}:{1}.obtenerReporte()", NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name);
          return;
        }
        // cantCamposHoja lo llena quien IMPLEMENTA esta clase (en método "template" InicializarEstructurasDatosReporte() 
        // que lo debe implementar quien extienda esta clase)
        ////cantCamposHoja = configReporteCompacto.ColumnasNombresCamposExcel.Length;

        //formatoFechaDDMMSSSSConGuion = (XSSFDataFormat)plantillaXltxWb.CreateDataFormat();
        //estiloFechaDDMMSSSSConGuion = (XSSFCellStyle)plantillaXltxWb.CreateCellStyle();

        //estiloFechaDDMMSSSSConGuion.DataFormat = formatoFechaDDMMSSSSConGuion.GetFormat(ConfReporteBase.FORMATO_SOLO_FECHA_CON_GUION);
        //estiloFechaDDMMSSSSConGuion.SetDataFormat(estiloFechaDDMMSSSSConGuion.DataFormat);
        //cellStyle.setDataFormat(creationHelper.createDataFormat().getFormat("dddd dd/mm/yyyy"));
        nroColumnaInicial = columnaInicial - 'A';
        //List<_BasePkServicioEntity> registrosServicios = ListaServiciosBase;
        var cantRegistrosServicios = ListaServiciosBase.Count;
        //= registrosReporteExtendidoSP.GroupBy(s => s.ServicioId);

        string datosInicialesStr = string.Format("<{4}:{5}> => Cantidad de servicios encontrados:{0}\nColumna Inicial: {1}={2}\nFila inicial iteracion: {3}"
          , cantRegistrosServicios
          , columnaInicial
          , nroColumnaInicial
          , nroFilaInicial
          , NOMBRE_CLASE_REP_EXT_INSUMO
          , GetType().Name);

        Console.WriteLine(datosInicialesStr);
        Log.Information(datosInicialesStr);

        CantidadExcelsGenerados = 0;
        RespaldarLibro();
        while (tiempoTranscurridoEsperandoAlgunArchivoRepExt <= cantTiempoMaximoSecsEsperandoAlgunArchivoRepExt)
        {
          foreach (var regSrvIter in ListaServiciosBase)
          {
            RegistroServIter = regSrvIter;
            try
            {
              if (EstadoProcesoReporte == PROCESO_REP_EN_PROCESO) continue;
               formatoDecimal = PlantillaXltxWb.CreateDataFormat().GetFormat("0.00");
              formatoFecha = PlantillaXltxWb.CreateDataFormat().GetFormat(ConfReporteBase.FORMATO_SOLO_FECHA_MINUCULA);
              string fileNameReporte = string.Format("{0}{1}{2}.{3}", rutaCarpetaRaizDestino, FileSystemHelper.DIR_SEPARATOR, regSrvIter.Int64PK, extensionArchivoReporte);

              //ObtenerDatosSP();

              //Se arma la ruta completa ("ruta carpeta convertida en absoluta" + "\" + "${IdServicio}.xltx" 
              //para leer el INSUMO REPORTE EXTENDIDO del srv IdServicio = regSrvIter.Int64PK
              fileNameExcelRepExt = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(string.Format("{0}{1}{2}.{3}"
                , rutaCarpetaRaizOrigen, Path.DirectorySeparatorChar, regSrvIter.Int64PK, CurrConfGlobal.ExtensionArchivosExcel), inicioIndiceStr);
              if (!File.Exists(fileNameExcelRepExt) && !repExtendidoExiste.ContainsKey(regSrvIter.Int64PK))
              {
                strMsj = string.Format("<{0}:{1}> Archivo excel (xlsx) insumo (reporte extendido) \"{2}\". No existe AUN. Pasando a sgte archivo"
                  , NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, fileNameExcelRepExt);
                Console.WriteLine(strMsj);
                Log.Information(strMsj);
                repExtendidoExiste[regSrvIter.Int64PK] = false;
                //continue;
              } else if (File.Exists(fileNameExcelRepExt))
              {
                repExtendidoExiste[regSrvIter.Int64PK] = true;
              }
              
              if (!repExtendidoExiste.ContainsKey(regSrvIter.Int64PK) || !repExtendidoExiste[regSrvIter.Int64PK])
              {
                continue;
              }
             
              if (dictServiciosGenerados.ContainsKey(regSrvIter.Int64PK) && dictServiciosGenerados[regSrvIter.Int64PK]) continue;

              var fileNameReporteOrig = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(fileNameReporte);
              //reporte a GENERAR (NO ES el reporte EXTENDIDO a leer)
              if (File.Exists(fileNameReporte))
              {
                repExtendidoExiste[regSrvIter.Int64PK] = true;
                try
                {
                  string fechaGeneracion = string.Empty;
                  DateTime dtFechaGeneracionRep;
                  using (FileStream fileStream = new FileStream(fileNameReporte, FileMode.Open, FileAccess.Read))
                  {
                    XSSFWorkbook excelLeidoConExito = (XSSFWorkbook)WorkbookFactory.Create(fileStream);
                    cell = excelLeidoConExito.GetSheetAt(0).GetRow(nroFilaFraseFechaGeneracion).GetCell(nroColumnaFraseFechaGeneracion);
                    strComentarioFechaMod = cell.StringCellValue;
                    strMsj = string.Format("<{0}:{1}> Archivo excel existente (xlsx) \"{2}\"\ntiene comentario de generacion: \"{3}\""
                      , NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, fileNameReporte, strComentarioFechaMod);
                    Console.WriteLine(strMsj);
                    Log.Information(strMsj);
                    Match match = Regex.Match(strComentarioFechaMod, fraseGeneracionFechaRegexPattern);
                    fechaGeneracion = match.Groups[1].Value;
                    excelLeidoConExito.Close();
                  }

                  //DateTime dtFechaGeneracionRep = DateTime.MinValue;
                  //DateTime.TryParseExact(fechaGeneracion
                  //  , ConfReporteBase.FORMATO_SOLO_FECHA_CON_GUION
                  //  , CultureInfo.InvariantCulture
                  //  , DateTimeStyles.None, out dtFechaGeneracionRep);
                  fechaGeneracion = string.IsNullOrWhiteSpace(fechaGeneracion) ? DateTime.Now.ToString(ConfReporteBase.FORMATO_SOLO_FECHA_CON_GUION) : fechaGeneracion;
                  DateTime.TryParseExact(fechaGeneracion, ConfReporteBase.FORMATO_SOLO_FECHA_CON_GUION, CultureInfo.InvariantCulture
                    , DateTimeStyles.None, out dtFechaGeneracionRep);
                  DateTime fechaArchivoRepExt = File.GetLastWriteTime(fileNameExcelRepExt);

                  //FileInfo fileInfoRep = new FileInfo(fileNameReporte);
                  
                  string carpetaFileInfoRep = Path.GetDirectoryName(fileNameReporte) + FileSystemHelper.DIR_SEPARATOR
                    + fechaGeneracion + FileSystemHelper.DIR_SEPARATOR 
                    + "Bak_" + DateTime.Now.ToString(ConfReporteBase.FORMATO_FECHA_HORA_CON_GUION_FILESSYTEM) + FileSystemHelper.DIR_SEPARATOR;
                  string fileFullPathRepRespaldo = carpetaFileInfoRep + Path.GetFileName(fileNameReporte); ;
                  carpetaFileInfoRep = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(carpetaFileInfoRep);
                  fileFullPathRepRespaldo = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(fileFullPathRepRespaldo);
                  strMsj = string.Format("<{0}:{1}> Archivo excel (xlsx) \"{2}\". Ya existe " +
                    "y fue generado el {3:dd-MM-yyyy HH:mm:ss} y fecha de escritura de\nreporte extendido {4} es {5:dd-MM-yyyy HH:mm:ss}. Por " +
                    "lo tanto {6} se volvera a generar. Se movera archivo de {1} a {7}"
                    , NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, fileNameReporte, dtFechaGeneracionRep.Date, fileNameExcelRepExt, fechaArchivoRepExt, 
                    dtFechaGeneracionRep.Date < fechaArchivoRepExt ? "SI" : "NO", carpetaFileInfoRep);
                  Console.WriteLine(strMsj);
                  Log.Information(strMsj);

                  EstadoProcesoReporte = dtFechaGeneracionRep.Date < fechaArchivoRepExt ? PROCESO_REP_INICIADO : PROCESO_REP_YA_ESTA_ACTUALIZADO | PROCESO_REP_TERMINADO;
                  if (EstadoProcesoReporte == PROCESO_REP_INICIADO)
                  {
                    Directory.CreateDirectory(carpetaFileInfoRep);
                    if (File.Exists(fileFullPathRepRespaldo))
                    {
                      var fileFullPathParaNoChocar = string.Format("{0}{1}{2}{1}{3}"
                        , fileFullPathRepRespaldo.Substring(0, fileFullPathRepRespaldo.LastIndexOf('.'))
                        , '.', GeneralHelper.RANDOM_NUMBER_GENERATOR.Next(), Path.GetExtension(fileNameReporte));
                      File.Move(fileNameReporte, fileFullPathParaNoChocar);
                    }
                    else
                    {
                      File.Move(fileNameReporte, fileFullPathRepRespaldo);
                    }
                  }
                }
                catch (Exception ex) when (ex is InvalidFormatException || ex is IOException || ex is NullReferenceException || ex is ArgumentOutOfRangeException || ex is SystemException)
                {
                  EstadoProcesoReporte = PROCESO_REP_INICIADO;
                  if (dictServiciosGenerados.ContainsKey(regSrvIter.Int64PK))
                  {
                    dictServiciosGenerados.Remove(regSrvIter.Int64PK);
                  }
                  strMsj = string.Format("\\(O.o)/ <{0}:{1}> => Error al tratar de leer archivo excel ya existente (xlsx) \"{2}\". SE GENERARA DE NUEVO.\nExcepcion={3}. Traza:\n{4}"
                    , NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, fileNameReporte, ex.GetType().AssemblyQualifiedName + ":" + ex.Message, ex.StackTrace);
                  Console.WriteLine(strMsj);
                  Log.Warning(strMsj);
                }
              }
              else
              {
                if (setServiciosEsperando.Contains(RegistroServIter.Int64PK))
                {
                  setServiciosEsperando.Remove(RegistroServIter.Int64PK);
                  EstadoProcesoReporte = PROCESO_REP_INICIADO;
                }
                else if (EstadoProcesoReporte < PROCESO_REP_EN_PROCESO)
                {
                  EstadoProcesoReporte = PROCESO_REP_INICIADO;
                }
              }

              fileNameReporte = fileNameReporteOrig;
              if (EstadoProcesoReporte == (PROCESO_REP_YA_ESTA_ACTUALIZADO | PROCESO_REP_TERMINADO))
              {
                string mensajeTerminoDeUnExcel = string.Format("<{0}:{1}> No se generara archivo excel \"{2}\" pues ya esta Generado y Actualizado. nroExcelsGenerados={3}"
                  ,NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, Path.GetFileName(fileNameReporte), CantidadExcelsGenerados);
                Console.WriteLine(mensajeTerminoDeUnExcel);
                Log.Information(mensajeTerminoDeUnExcel);
                EstadoProcesoReporte = 0;
                dictServiciosGenerados[regSrvIter.Int64PK] = true;
                continue;
              }

              CantidadExcelsGenerados = dictServiciosGenerados.Where(item => item.Value).Count();

              bool esTurnoDeExcel = !dictServiciosGenerados.ContainsKey(RegistroServIter.Int64PK)
                && EstadoProcesoReporte == PROCESO_REP_INICIADO;

              if (esTurnoDeExcel)
              {
                dictServiciosGenerados[RegistroServIter.Int64PK] = false;
                
                EstadoProcesoReporte = PROCESO_REP_EN_PROCESO;
                strMsj = string.Format("<{0}:{1}> El archivo excel \"{2}\" AHORA se encuentra en PROCESO DE GENERACION\n\tPERO SE VERA SI SE PUEDE LEER reporte extendido de dicho Id Servicio. nroExcelsGenerados={3}"
                    , NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, fileNameReporte, CantidadExcelsGenerados);
                Console.WriteLine(strMsj);
                Log.Information(strMsj);
                if (setServiciosEsperando.Contains(RegistroServIter.Int64PK)) setServiciosEsperando.Remove(RegistroServIter.Int64PK);
                //if (!dictServiciosGenerados.ContainsKey(regSrvIter.Int64PK))
                //{
                //	strMsj = string.Format("=> {0} Se empezara a generar archivo excel (xlsx) \"{1}\". nroRegsSP \"{2}\"={3}", GetType().Name, fileInfoRepComp.Name, nombreSP, cantRegistrosSP);
                //	Console.WriteLine(strMsj);
                //	Log.Information(strMsj);
                //	dictServiciosGenerados[regSrvIter.Int64PK] = false;
                //} else
                //{
                //	Console.WriteLine("Se esta generando otro excel de {2} El excel \"{0}\". debe esperar a que el excel actual se genere. nroExcelsGenerados={1}"
                //		, fileInfoRepComp.Name
                //		, dictServiciosGenerados.Where(item => item.Value).Count()
                //		);
                //	Log.Information("Se esta generando otro excel de Rep. {2} Compacto El excel \"{0}\". debe esperar a que el excel actual se genere. nroExcelsGenerados={1}", fileInfoRepComp.Name, dictServiciosGenerados.Where(item => item.Value).Count());
                //	continue;
                //}
              }
              else
              {
                if (!setServiciosEsperando.Contains(RegistroServIter.Int64PK))
                {
                  strMsj = string.Format("<{0}:{1}> Se esta generando otro excel de {1} El excel \"{2}\". Debe esperar a que el excel actual se genere. nroExcelsGenerados={3}"
                    , NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, Path.GetFileName(fileNameReporte), CantidadExcelsGenerados);
                  Console.WriteLine(strMsj);
                  Log.Debug(strMsj);
                  setServiciosEsperando.Add(RegistroServIter.Int64PK);
                }
                continue;
              }
              int columnaInicialInsumoRepExt = CellReference.ConvertColStringToIndex(configReporteExtendido.ColumnaInicialExcelIteracionDatos);

              // RECIEN SI YA HAY UN ARCHIVO EXCEL GENERADO OK para el servicio y no existe el excel de reporte compacto para tal servicio
              // Se obtienen los datos del excel de reporte compacto
              ////List<RegistroCompraProrrateada> conjuntoComprasProrrateadas =
              ////	ExcelHelper.ObtenerInfoDesdeInsumoXlsx<RegistroCompraProrrateada>(fileNameExcelRepExt
              ////																				, columnaInicialInsumoRepExt, configReporteExtendido.FilaInicialExcelIteracionDatos - 1
              ////																				, configReporteCompacto.ColumnasRepExtInsumo);

              strMsj = string.Format("<{0}:{1}> Obteniendo Compras prorrateadas de Rep. Ext \"{2}\" \npara generar=> \"{3}\"\n\tNroExcelsGenerados={4}"
                  , NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, fileNameExcelRepExt, fileNameReporte, CantidadExcelsGenerados);
              Console.WriteLine(strMsj);
              Log.Information(strMsj);
              dictServiciosGenerados[regSrvIter.Int64PK] = false;
              try
              {
                conjuntoComprasProrrateadas =
                  ExcelHelper.ObtenerInfoDesdeInsumoXlsx<DTO>(fileNameExcelRepExt
                                                        , columnaInicialInsumoRepExt, configReporteExtendido.FilaInicialExcelIteracionDatos - 1
                                                        , columnasRepExtInsumo);
                strMsj = string.Format("<{0}:{1}> Compras prorrateadas OBTENIDAS CON EXITO de Rep. Ext \"{2}\"\n\tpara generar=> \"{3}\"\n\tNroExcelsGenerados={4}"
                    , NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, fileNameExcelRepExt, fileNameReporte, CantidadExcelsGenerados);
                if (setServiciosEsperando.Contains(RegistroServIter.Int64PK)) setServiciosEsperando.Remove(RegistroServIter.Int64PK);
                EstadoProcesoReporte = PROCESO_REP_EN_PROCESO;
                Console.WriteLine(strMsj);
                Log.Information(strMsj);
              } catch (IOException ioe)
              {
                strMsj = string.Format("\\(O.o)/ <{0}:{1}> => Error al tratar de leer archivo excel (xlsx) insumo (reporte extendido) \"{2}\" ({3}): Archivo en USO u otro error. Traza:\n{4}"
                  , NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, fileNameExcelRepExt, ioe.GetType().AssemblyQualifiedName, ioe.StackTrace);
                Console.WriteLine(strMsj);
                Log.Warning(strMsj);
                dictServiciosGenerados[regSrvIter.Int64PK] = false;
                EstadoProcesoReporte = 0;
                if (!setServiciosEsperando.Contains(RegistroServIter.Int64PK)) setServiciosEsperando.Add(RegistroServIter.Int64PK);
                continue;
              }

              RespaldarLibro(true);
              #region llamada a logica que maneja/modela/procesa los datos un xlsx de ReporteExtendido (y pinta el reporte que coresponda a su estension de esta clase)
              /**
							 * SE DEBE IMPLEMENTAR ESTE METODO PARA TODOS LOS QUE QUIERAN HERERDAR ESTA CLASE SI QUIEREN
							 * OBTENER UNA LISTA DE OBJETOS DE UN EXCEL DE PRORRATEO (xlsx de ReporteExtendido)
							 * Y MANEJAR LOS DATOS EN EL (PATRON TEMPLATE de PROGRAMACION)
							 * **/
              strMsj = string.Format("<{0}:{1}> Obteniendo datos SP para  \"{2}\"\n\tNroExcelsGenerados={3}"
                  , NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, fileNameReporte, CantidadExcelsGenerados);
              Console.WriteLine(strMsj);
              Log.Information(strMsj);
              ObtenerDatosSP();
              strMsj = string.Format("<{0}:{1}> Invocando LogicaReporte para \"{2}\"\n\tNroExcelsGenerados={3}"
                  , NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, fileNameReporte, CantidadExcelsGenerados);
              Console.WriteLine(strMsj);
              Log.Information(strMsj);
              LogicaReporte();
              strMsj = string.Format("<{0}:{1}> Terminada LogicaReporte para \"{2}\"\n\tNroExcelsGenerados={3}"
                  , NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, fileNameReporte, CantidadExcelsGenerados);
              Console.WriteLine(strMsj);
              Log.Information(strMsj);
              #endregion

              fileNameReporte = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(fileNameReporte);
              using (FileStream fileStream = new FileStream(fileNameReporte, FileMode.OpenOrCreate, FileAccess.Write))
              {
                PlantillaXltxWb.Write(fileStream);
                fileStream.Close();
              }
              EstadoProcesoReporte = PROCESO_REP_TERMINADO;
              dictServiciosGenerados[regSrvIter.Int64PK] = true;
              RestaurarLibro();
              var mensajePlantillaLoopSrv = string.Format("<{0}:{1}> Reporte \"{2}\" Generado y Grabado"
                , NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, fileNameReporte);
              setServiciosEsperando.Remove(regSrvIter.Int64PK);
              Console.WriteLine(mensajePlantillaLoopSrv);
              Log.Information(mensajePlantillaLoopSrv);
            }
            catch (Exception exLoop)
            {
              var mensajePlantillaExLoopSrv = string.Format("\\(O.o)/ <{6}:{5}> => Excepcion al procesar compras de Servicio IdServicio={0}, NombreServicio=[{1}], AnexoId:{2} "
                + "TipoExcepcion={3}. TRAZA:\n{4}", regSrvIter.Int64PK, regSrvIter.NombreServicio, regSrvIter.AnexoId
                , exLoop.GetType().AssemblyQualifiedName, exLoop, exLoop.StackTrace, NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name);
              Console.WriteLine(mensajePlantillaExLoopSrv);
              Log.Error(mensajePlantillaExLoopSrv);
            }
          } //foreach de cada servicio id
          long ticksInicioSleep = DateTime.Now.Ticks;
          Thread.Sleep(intervaloTiempoReintentoEncontrarAlgunArchivoRepExtMseg);
          long difTicks = DateTime.Now.Ticks - ticksInicioSleep;
          TimeSpan difTimeSpan = TimeSpan.FromTicks(difTicks);
          tiempoTranscurridoEsperandoAlgunArchivoRepExt += Convert.ToInt32(Math.Round(difTimeSpan.TotalSeconds)); //(1 segundo);
          CantidadExcelsGenerados = dictServiciosGenerados.Where(item => item.Value).Count();
          if ((tiempoTranscurridoEsperandoAlgunArchivoRepExt % 10) == 0)
          {
            string mensajeCuentaTiempo = string.Format("<{0}:{1}> => Van {2} segundos. Maximo a esperar {3} segundos. Excels generados: {4}"
              , NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, tiempoTranscurridoEsperandoAlgunArchivoRepExt, cantTiempoMaximoSecsEsperandoAlgunArchivoRepExt, CantidadExcelsGenerados);
            Console.WriteLine(mensajeCuentaTiempo);
            Log.Information(mensajeCuentaTiempo);
          }
          //string mensajeEspera = string.Format("cantidadExcelsGenerados={0}/{1}, tiempoTranscurridoEsperandoAlgunArchivoRepExt={2} segs. Tiempo Intervalo Espera:{3} segs"
          //	, cantidadExcelsGenerados, registrosServicios.Count(), tiempoTranscurridoEsperandoAlgunArchivoRepExt, intervaloTiempoReintentoEncontrarAlgunArchivoRepExtSegs);
          //Console.WriteLine(mensajeEspera);
          //Log.Information(mensajeEspera);
          if (CantidadExcelsGenerados >= cantRegistrosServicios)
          {
            break;
          }
        }
        string mensajeTermino = string.Format("<{0}:{1}> => {2} de {3} archivos excel generados para \"{4}\", tiempoTranscurrido={5} segs"
          , NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, CantidadExcelsGenerados, cantRegistrosServicios, GetType().Name, tiempoTranscurridoEsperandoAlgunArchivoRepExt);
        Console.WriteLine(mensajeTermino);
        Log.Information(mensajeTermino);

      }
      catch (Exception ex)
      {
        var mensajeExcepcionExterna = string.Format("\\(O.o)/ <{0}:{1}> => Excepcion al procesar compras "
          + "TipoExcepcion={2}. TRAZA:\n{3}", NOMBRE_CLASE_REP_EXT_INSUMO, GetType().Name, ex.GetType().AssemblyQualifiedName, ex.StackTrace);
        Console.WriteLine(mensajeExcepcionExterna);
        Log.Fatal(mensajeExcepcionExterna);
      }
    }
  }
}
