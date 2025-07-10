using OrquestadorGesp.ContextEFNetCore;
using Microsoft.EntityFrameworkCore;
using NPOI.XSSF.UserModel;
using OrquestadorGesp.AppSettingsJson;
using OrquestadorGesp.Helpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OrquestadorGesp.Reportes
{
	public abstract class ReporteBase
	{
		protected int cantCamposHoja = 0;
		private XSSFWorkbook plantillaXltxWbOrig = null;
		protected XSSFWorkbook PlantillaXltxWb { get; set; }
		protected short formatoDecimal;
		protected short formatoFecha;
		//private XSSFSheet hojaExcel;
		//private XSSFSheet hojaExcelBackup;
		protected XSSFSheet HojaExcel { get; set; }
		//	set {
		//		hojaExcel = value;
		//		if (hojaExcelBackup == null)
		//		{
		//			nombreHojaOriginal = hojaExcel.SheetName;
		//			hojaExcelBackup = (XSSFSheet)PlantillaXltxWb.CloneSheet(0, nombreHojaOriginal+"_backup");
		//			hojaExcelBackup.GetWorkbook().SetSheetHidden(1, true);
		//			hojaExcelBackup.GetWorkbook().SetActiveSheet(0);
		//		}
		//	}
		//	get { return (XSSFSheet)PlantillaXltxWb.GetSheetAt(0); }
		//}
		protected int nroFilaInicial;
		protected int nroColumnaInicial;
		protected int cantRegistrosSP = 0;
		private static ConfJson currConfGlobal;
		protected ConfReporteBase ConfReporte;
		protected string nombreSP;
		protected string nombreCSV;
		protected List<_BaseEntity> registrosOrigen = new List<_BaseEntity>();
		//protected static GespDbContext dbContext = null;
		protected GespDbContext dbContext = null;
    protected GespDbContext DbContextInstance { get => dbContext; }

    private const string plantillaCodSqlEjecutarSpSinParams = "EXECUTE {0}";
		//protected _BasePkServicioEntity registroServIter;
		protected static List<_BasePkServicioEntity> ListaServiciosBase = new List<_BasePkServicioEntity>();

		public static string PLANTILLA_COD_SQL_EJECUTAR_SP_SIN_PARAMS => plantillaCodSqlEjecutarSpSinParams;

		public static ConfJson CurrConfGlobal
		{ get => currConfGlobal;
			set  {
				if (currConfGlobal == null)
				{
					currConfGlobal = value;
				}
			}
		}

		protected ReporteBase(bool limpiarIdsServicios)
		{
			if (dbContext == null)
			{
				Inicializar();
			}

			if (limpiarIdsServicios)
			{
				Inicializar(false);
			}
			ObtenerOrigenDatos();
		}

		protected ReporteBase()
		{
			if (CurrConfGlobal == null)
			{
				string mensajeConf = string.Format("Obteniendo configuracion para {0} desde constructor sin parametros (por defecto)", GetType().Name);
				Log.Information(mensajeConf);
				Console.WriteLine(mensajeConf);
				CurrConfGlobal = GeneralHelper.ObtenerConfJson();
				CurrentConfJson.CurrentConf = CurrConfGlobal;
			}
      dbContext = GespDbContext.GetInstance();
      dbContext.Database.SetCommandTimeout(CurrConfGlobal.DBTimeoutMsec);
    }

    private void Inicializar(bool desdeCero = true)
		{
			if (desdeCero) {
				string mensajeConf = string.Format("Obteniendo configuracion para {0} desde constructor con parametro \"bool limpiarIdsServicios\"", GetType().Name);
				Log.Information(mensajeConf);
				Console.WriteLine(mensajeConf);
				CurrConfGlobal = GeneralHelper.ObtenerConfJson();
				CurrentConfJson.CurrentConf = CurrConfGlobal; 
				dbContext = GespDbContext.GetInstance();
				dbContext.Database.SetCommandTimeout(CurrConfGlobal.DBTimeoutMsec);
			}
			LlenarIdsServicios(); //, nombreAliasTablaServicio);
		}

		protected void LlenarIdsServicios() //, string nombreAliasTablaServicio)
		{
      if (ListaServiciosBase == null || ListaServiciosBase.Count == 0)
      {
        string commandStr = CurrConfGlobal.QueryIdsTodosLosServicios;
        Console.WriteLine("Por ejecutar comando SQL: {0}", commandStr);
        Log.Information("Por ejecutar comando SQL: {0}", commandStr); //
        IQueryable<_BasePkServicioEntity> entitiesServicios = dbContext.ServicioInfoBaseSet.FromSql(commandStr);
        ListaServiciosBase = entitiesServicios.ToList();
        entitiesServicios = null;
      }
    }
    private void ObtenerOrigenDatos() {
			if (CurrConfGlobal == null) CurrConfGlobal = CurrentConfJson.CurrentConf;
			ObtenerConfReporte();
			if (ConfReporte == null)
			{
				throw new ArgumentException("Debe inicializar la configuracion del reporte antes de obtener el origen de datos");
			}
			nombreSP = ConfReporte.NombreProcedimientoAlmacenado;
			nombreCSV = ConfReporte.RutaCompletaCSV;
			nroFilaInicial = ConfReporte.FilaInicialExcelIteracionDatos - 1;
			if (!ConfReporte.Active) return;

			if (!string.IsNullOrWhiteSpace(nombreSP))
			{
				ObtenerDatosSP();
			}
			if (!string.IsNullOrWhiteSpace(nombreCSV))
			{
				ObtenerDatosCSV();
			}
		}

    public abstract void TerminarGeneracionReporte();
      
    public abstract void ObtenerConfReporte();
		public abstract void ObtenerDatosSP();
		public abstract void ObtenerDatosCSV();
		public abstract void ObtenerReporte();

		protected void RestaurarLibro()
		{
			if (!ConfReporte.Active) return;
			if (plantillaXltxWbOrig == null)
			{
				throw new InvalidOperationException("No se puede obtener el respaldo libro de excel inicial pues nunca se invoco el metodo RespaldarLibro");
			}
			else
			{
				PlantillaXltxWb.Close();
        PlantillaXltxWb = null;
        MemoryStream ms = new MemoryStream();
				plantillaXltxWbOrig.Write(ms, true);
				ms.Seek(0, SeekOrigin.Begin);
				PlantillaXltxWb = new XSSFWorkbook(ms);
				ms.Close();
				ms.Dispose();
        ms = null;
				//while (PlantillaXltxWb.NumberOfSheets > 0)
				//{
				//	try
				//	{
				//		PlantillaXltxWb.RemoveSheetAt(0);
				//	}
				//	catch (Exception ex)
				//	{
				//		string mensajeFalloBorrarPrimeraHoja = string.Format("Couldn't remove sheet 0. Remaining sheets: {0}", PlantillaXltxWb.NumberOfSheets);
				//		Console.WriteLine("{0}, Exception:{1}\n{2}", mensajeFalloBorrarPrimeraHoja, ex.Message, ex.StackTrace);
				//		Log.Warning("{0}, Exception:{1}\n{2}", mensajeFalloBorrarPrimeraHoja, ex.Message, ex.StackTrace);
				//	}
				//}
				////List<IName> nombresConInfo = PlantillaXltxWb.GetAllNames().ToList();
				////foreach (var nombreConInfo in nombresConInfo)
				////{
				////	PlantillaXltxWb.RemoveName(nombreConInfo);
				////}
				//for (var i = plantillaXltxWbOrig.NumberOfSheets - 1; i >= 0; i--)
				//{
				//	var sheet = plantillaXltxWbOrig.GetSheetAt(i);
				//	var sheetname = sheet.SheetName;
				//	sheet.CopyTo(PlantillaXltxWb, sheetname, true, true);
				//}
				//for (int i = plantillaXltxWbOrig.NumCellStyles-1; i >=0 ; i--)
				//{
				//	var cellStyle = plantillaXltxWbOrig.GetCellStyleAt(i);
				//	PlantillaXltxWb.GetCellStyleAt(i).CloneStyleFrom(cellStyle);
				//}

				//plantillaXltxWb = plantillaXltxWbBak
				//var uid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
				//var uid2 = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
				//PlantillaXltxWb.RemoveAt(0);
				//ISheet hojaVacia = PlantillaXltxWb.GetSheetAt(0);
				////ISheet hojaTrabajo = plantillaXltxWb.GetSheetAt(0);
				//hojaVacia.CopyTo(PlantillaXltxWb, nombreHojaOriginal, true, true);
				//PlantillaXltxWb.SetSheetHidden(0, false);
				//PlantillaXltxWb.SetSheetHidden(1, false);
				//PlantillaXltxWb.SetSheetName(0, uid);
				//PlantillaXltxWb.SetSheetName(1, uid2);
				//PlantillaXltxWb.SetSheetName(0, nombreHojaOriginal);
				//PlantillaXltxWb.SetSheetName(1, nombreHojaOriginal+"_backup");

			}
		}

    protected void DejarLogRegistroEscrituraCompletaExcel(int cantFilasEscritas, _BasePkServicioEntity srvIterEntity)
    {
      string msjCantFilasEscritasExcel = String.Format(
        "<{0}:{1}> Se escribieron {2} filas de Excel para el Servicio \"{3}\" (id={4})"
        , typeof(ReporteBase).Name
        , GetType().Name
        , cantFilasEscritas
        , srvIterEntity?.NombreServicio
        , srvIterEntity?.Int64PK);
      Console.WriteLine(msjCantFilasEscritasExcel);
      Log.Information(msjCantFilasEscritasExcel);
    }
    protected void RespaldarLibro(bool respaldoDeNuevo = false)
		{
			if (!ConfReporte.Active) return;
			if (plantillaXltxWbOrig == null || respaldoDeNuevo)
			{
				MemoryStream ms = new MemoryStream();
				PlantillaXltxWb.Write(ms);
				ms.Position = 0;
				plantillaXltxWbOrig = new XSSFWorkbook(ms);
			} else
			{
				//	throw new InvalidOperationException("Solo puede obtener UNA VEZ una copia de la referencia al libro de excel actual. Dicha copia se gatilla " +
				string mensajeAdvertencia = "Solo se puede respaldar UNA VEZ el libro de excel actual. El respaldo quedara " +
					"como un objeto con el estado del libro al momento que se invoque este metodo (sus hojas, estilos, etc). Luego puede restaurarlo " +
					"con el metodo LimpiarLibro";
				Console.WriteLine(mensajeAdvertencia);
				Log.Warning(mensajeAdvertencia);
			}
		}

		//plantillaXltxWb.Clear();
		//plantillaXltxWb.Insert(0, hojaExcel);
		//int indiceHojaAntigua = plantillaXltxWb.IndexOf(hojaExcel);
		//if (indiceHojaAntigua > 0)
		//{
		//	plantillaXltxWb.RemoveAt(indiceHojaAntigua);
		//}

		//if (nroRegUnidadServ > 1)
		//{
		//	string msjUltimaFilaExcel = String.Format("Ultimo nro de fila en Archivo Excel antes de borrar filas desde fila {0} (parte desde 0): {1}", nroFilaInicial + 1, hojaExcel.LastRowNum + 1);
		//	Console.WriteLine(msjUltimaFilaExcel);
		//	Log.Information(msjUltimaFilaExcel);
		//	int cantFilasEliminadas = ExcelHelper.MoverUltimaFilaHastaIndice(hojaExcel, nroFilaInicial);
		//	string msjCantFilasEliminadas = String.Format("Eliminadas {0} filas de Excel desde fila {1} (parte desde 1) para Servicio \"{2}\" (id={3})\ndespues de rellenar con {4} filas"
		//		, cantFilasEliminadas
		//		, nroFilaInicial + 1
		//		, nombreServicio, servicioId, nroRegUnidadServ);
		//	Console.WriteLine(msjCantFilasEliminadas);
		//	Log.Information(msjCantFilasEliminadas);
		//	msjUltimaFilaExcel = String.Format("Ultimo nro de fila en Archivo Excel DESPUES de borrar filas (parte desde 1): {0}", hojaExcel.LastRowNum + 1);
		//	Console.WriteLine(msjUltimaFilaExcel);
		//	Log.Information(msjUltimaFilaExcel);
		//}
		//for (int i = 0; i < cantCamposHoja; i++)
		//{
		//	hojaExcel.GetRow(nroFilaInicial).GetCell(nroColumnaInicial + i).SetCellType(CellType.Blank);
		//}
	}
}
