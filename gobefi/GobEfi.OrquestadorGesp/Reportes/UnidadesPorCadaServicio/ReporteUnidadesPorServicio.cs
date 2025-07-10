using OrquestadorGesp.ContextEFNetCore;
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
using System.Threading;

namespace OrquestadorGesp.Reportes
{
	public class ReporteUnidadesPorServicio : ReporteBase
	{
		public List<UnidadesPorCadaServicioEnt> registrosUnidadesPorServicioGlobal = null;
		public ReporteUnidadesPorServicio() : base(false) { }
		public ReporteUnidadesPorServicio(bool borrarListaServicios) : base(borrarListaServicios)	{	}

		public override void ObtenerConfReporte()
		{
			ConfReporte = CurrConfGlobal.ReporteUnidadesPorServicio;
		}
		public override void ObtenerDatosSP()
		{
			var commandStr = string.Format(PLANTILLA_COD_SQL_EJECUTAR_SP_SIN_PARAMS, nombreSP);
			var msjCommandStr = string.Format("{0}. Comando SQL A Ejecutar: {1}", this.GetType().Name, commandStr);
			Console.WriteLine(msjCommandStr);
			Log.Information(msjCommandStr);
			registrosUnidadesPorServicioGlobal = dbContext.UnidadesPorServicioSet.FromSql(PLANTILLA_COD_SQL_EJECUTAR_SP_SIN_PARAMS, nombreSP).ToList();
		}


		public override void ObtenerDatosCSV()
		{
		}

		public override void ObtenerReporte()
		{
			if (registrosUnidadesPorServicioGlobal == null)
			{
				var mensajeNoHayRegistrosFaltaInvocacionInicial = "No hay registros para ReporteUnidadesPorCadaServicio o bien no se invoco metodo ObtenerDatosSP";
				var mensajeSaliendoMetodo = "Saliendo de ReporteUnidadesPorCadaServicio.ObtenerReporte()";
				Console.WriteLine(mensajeNoHayRegistrosFaltaInvocacionInicial);
				Console.WriteLine(mensajeSaliendoMetodo);
				Log.Warning(mensajeNoHayRegistrosFaltaInvocacionInicial);
				Log.Warning(mensajeSaliendoMetodo);
				return;
			}
			var baseDir = AppContext.BaseDirectory + Path.DirectorySeparatorChar;
			var rutaPlantillasReportes = CurrConfGlobal.RutaPlantillasExcel;
			var subRutaPlantillaReporte = ConfReporte.SubRutaPlantillaExcel;
			if (!FileSystemHelper.RutaEsAbsoluta(rutaPlantillasReportes))
			{
				rutaPlantillasReportes = String.Format("{0}{1}", baseDir, rutaPlantillasReportes);
				rutaPlantillasReportes = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(rutaPlantillasReportes);
			}
			subRutaPlantillaReporte = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(subRutaPlantillaReporte);
			if (FileSystemHelper.RutaEsAbsoluta(subRutaPlantillaReporte))
			{
				Log.Warning("La configuracion \"SubRutaPlantillaExcel\" NO PUEDE SER ABSOLUTA. Valor en archivo de configuracion json:\n", subRutaPlantillaReporte);
				Log.Warning("Saliendo de ReporteUnidadesPorCadaServicio.obtenerReporte()");
				return;
			}

			#region Variables Configuracion Reporte
			string archivoPlantillaReporte = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(ConfReporte.ArchivoPlantillaExcel);
			string rutaPlantillaReporte = rutaPlantillasReportes.EndsWith(FileSystemHelper.DIR_SEPARATOR) ?
				GeneralHelper.RemoveLastChar(rutaPlantillasReportes): rutaPlantillasReportes;
			string subRutaPlantillaReporteUnidadesPorServ = subRutaPlantillaReporte.EndsWith(FileSystemHelper.DIR_SEPARATOR) ?
				GeneralHelper.RemoveLastChar(subRutaPlantillaReporte) : subRutaPlantillaReporte;
			rutaPlantillaReporte = string.Format("{0}{1}{2}{1}{3}"
				, rutaPlantillaReporte
				, FileSystemHelper.DIR_SEPARATOR
				, subRutaPlantillaReporte
				, archivoPlantillaReporte);
			var reporteUnidadesPorServConf = (ConfReporteUnidadesPorServicio)ConfReporte;
			string celdaFechaHoy = reporteUnidadesPorServConf.CeldaFechaHoyExcel;
			string celdaNombreServicio = reporteUnidadesPorServConf.CeldaNombreServicioExcel;
			string columnaInicialIteracion = ConfReporte.ColumnaInicialExcelIteracionDatos;
			string nombreCampoNombreServicio = reporteUnidadesPorServConf.NombreCampoNombreServicio;
			string nombreCampoIdServicio = reporteUnidadesPorServConf.NombreCampoIdServicio;
			string[] nombresCamposDesdeSPHaciaExcel = ConfReporte.ColumnasNombresCamposExcel;
			string formatoFechaDisplay = CurrConfGlobal.FormatoFechaDisplay;
			string extensionArchivoReporte = rutaPlantillaReporte.Substring(
				rutaPlantillaReporte.LastIndexOf('.') + 1
			);
			string mensajeRutaPlantillaReporte = string.Format("Ruta plantilla para Reporte {0}={1}", GetType().Name, rutaPlantillaReporte);
			string mensajeCarpetaDestinoReporte = string.Format("Carpeta raiz destino para Reporte {0}={1}", baseDir, subRutaPlantillaReporte);
			Console.WriteLine(mensajeRutaPlantillaReporte);
			Log.Information(mensajeRutaPlantillaReporte);
			string carpetaDestinoReportes = string.Format("{0}{1}", baseDir, subRutaPlantillaReporte);
			Console.WriteLine(carpetaDestinoReportes);
			Log.Information(carpetaDestinoReportes);
			#endregion
			using (FileStream file = new FileStream(rutaPlantillaReporte, FileMode.Open, FileAccess.Read))
			{
				PlantillaXltxWb = new XSSFWorkbook(file);
				file.Close();
			}
			// averiguar otra forma de agrupar ids y nombres servicios pues arroja solamente una ocurrencia ID: 3 ; Ejercito de Chile
			var columnaInicial = columnaInicialIteracion[0];
			if (columnaInicial > 'Z')
			{
				Log.Warning("La columna inicial debe estar entre A y Z. Valor de \"ColumnaInicialIteracionDatosSPUnidadesPorServicio\" en archivo de configuracion json excede limite superior:\n", columnaInicial);
				Log.Warning("Saliendo de ReporteUnidadesPorCadaServicio.obtenerReporte()");
				return;
			}

			if (columnaInicial < 'A')
			{
				Log.Warning("La columna inicial debe estar entre A y Z. Valor de \"ColumnaInicialIteracionDatosSPUnidadesPorServicio\" en archivo de configuracion json excede limite inferior:\n", columnaInicial);
				Log.Warning("Saliendo de ReporteUnidadesPorCadaServicio.obtenerReporte()");
				return;
			}
			cantCamposHoja = nombresCamposDesdeSPHaciaExcel.Length;
			formatoDecimal = PlantillaXltxWb.CreateDataFormat().GetFormat("0.00");
			formatoFecha = PlantillaXltxWb.CreateDataFormat().GetFormat(ConfReporteBase.FORMATO_SOLO_FECHA_MINUCULA);

			nroColumnaInicial = columnaInicial - 'A';
			var registrosServicios = ListaServiciosBase;//registrosUnidadesPorServicioGlobal.GroupBy(s => s.ServicioId);

			
			RespaldarLibro();
			foreach (var registroServ in registrosServicios)
			{
				HojaExcel = (XSSFSheet)PlantillaXltxWb.GetSheetAt(0);
				var hojaExcelSrv = HojaExcel;
				var registroServIter = registroServ;
				var registrosUnidadesServicios = registrosUnidadesPorServicioGlobal.Where(r => r.ServicioId == registroServIter.Int64PK).OrderBy(r => r.Int64PK);
				string datosInicialesStr = string.Format("Cantidad de servicios encontrados de {0} para servicio {1}: {2}\nColumna Inicial: {3}={4}\nFila inicial iteracion: {5}"
					, GetType().Name
					, registroServ.NombreServicio
					, registrosUnidadesServicios.Count()
					, columnaInicial
					, nroColumnaInicial
					, nroFilaInicial);
				Console.WriteLine(datosInicialesStr);
				Log.Information(datosInicialesStr);
				var refCeldaExcel = new CellReference(celdaFechaHoy);
				XSSFRow filaExcel = (XSSFRow)hojaExcelSrv.GetRow(refCeldaExcel.Row);
				XSSFCell celdaExcel = (XSSFCell)filaExcel.GetCell(refCeldaExcel.Col);
				string valorCeldaStr = DateTime.Now.ToString(formatoFechaDisplay);
				celdaExcel.SetCellValue(valorCeldaStr);
				refCeldaExcel = new CellReference(celdaNombreServicio);
				filaExcel = (XSSFRow)hojaExcelSrv.GetRow(refCeldaExcel.Row);
				celdaExcel = (XSSFCell)filaExcel.GetCell(refCeldaExcel.Col);
				celdaExcel.SetCellValue(registroServ.NombreServicio);
				string fileName = string.Format("{0}{1}{2}{3}.{4}", baseDir, subRutaPlantillaReporte, FileSystemHelper.DIR_SEPARATOR, registroServ.Int64PK, extensionArchivoReporte);
				XSSFRow filaExcelInicial = (XSSFRow)hojaExcelSrv.GetRow(nroFilaInicial);
				IRow filaExcelIter = filaExcelInicial;
				int nroFilaIter = nroFilaInicial;
				int cantRegistrosUnidadesServicios = registrosUnidadesServicios.Count();
				//string formatoLineaStr = "Info reg. nro {0} de {1}";
				//string formatoColumnaStr = "Propiedad {0} = [[{1}]]";
				//string formatoCeldaStr = "Celda {0}{1} es Nula? {2}";
				int nroRegUnidadServ = 0;
				Thread.Sleep(CurrentConfJson.CurrentConf.TiempoReposoHebrasParaNoAhogarCPUMsec);
				foreach (var registroUnidadServicio in registrosUnidadesServicios)
				{
					//Console.WriteLine(string.Format(formatoLineaStr, nroRegUnidadServ + 1, cantRegistrosUnidadesServicios));
					//Log.Information(string.Format(formatoLineaStr, nroRegUnidadServ + 1, cantRegistrosUnidadesServicios));
					Thread.Sleep(CurrentConfJson.CurrentConf.TiempoReposoHebrasParaNoAhogarCPUMsec);
					for (int i = 0; i < nombresCamposDesdeSPHaciaExcel.Length; i++)
					{
						Thread.Sleep(CurrentConfJson.CurrentConf.TiempoReposoHebrasParaNoAhogarCPUMsec);
						string nombreCampo = nombresCamposDesdeSPHaciaExcel[i];
						var valorParaEscribirCelda = i == 0 ? nroRegUnidadServ + 1 : GeneralHelper.ObtenerValorPropiedad(registroUnidadServicio, nombreCampo);
						Type tipoValorCelda = valorParaEscribirCelda?.GetType();
						var valorParaEscribirCeldaStr = Convert.ToString(valorParaEscribirCelda);
						if (ConfReporte.ColumnasNombresCamposExcelConDefaultSinInfo.Contains(nombreCampo))
						{
							if ("0".Equals(valorParaEscribirCeldaStr))
							{
								valorParaEscribirCelda = ConfReporteBase.VALOR_CAMPO_SIN_INFO;
								valorParaEscribirCeldaStr = ConfReporteBase.VALOR_CAMPO_SIN_INFO;
								tipoValorCelda = typeof(string);
							}
						}
						//Console.Write(string.Format(formatoColumnaStr, nombreCampo, valorParaEscribirCelda));
						//Log.Information("{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:Information}] {Message}",
						//string msjColummna = string.Format(formatoColumnaStr, nombreCampo, valorParaEscribirCelda);
						ICell celdaIter = filaExcelIter.GetCell(nroColumnaInicial + i);
						//string msjCelda = string.Format(formatoCeldaStr, (char)('A' + nroColumnaInicial + i), nroFilaIter + 1, celdaIter == null);
						//string msjIeracionCampo = string.Format("\t{0}, {1}", msjColummna, msjCelda);
						//Console.WriteLine(msjIeracionCampo);
						//Log.Debug(msjIeracionCampo);
						if (GeneralHelper.EsNumericoEntero(tipoValorCelda))
						{
							celdaIter.SetCellValue(long.Parse(valorParaEscribirCeldaStr));
							celdaIter.SetCellType(CellType.Numeric);
							celdaIter.CellStyle.DataFormat = ExcelHelper.FORMATO_NPOI_SIN_DECIMALES;
						}
						else if (GeneralHelper.EsNumericoDecimal(tipoValorCelda))
						{
							celdaIter.SetCellValue(double.Parse(valorParaEscribirCeldaStr));
							celdaIter.SetCellType(CellType.Numeric);
							celdaIter.CellStyle.DataFormat = ExcelHelper.FORMATO_NPOI_DOS_DECIMALES;
						}
						else if (tipoValorCelda == typeof(bool))
						{
							celdaIter.SetCellValue((bool)valorParaEscribirCelda ? "Sí" : "No");
						}
						else
						{
							celdaIter.SetCellValue(valorParaEscribirCeldaStr);
						}
					}
					nroRegUnidadServ++;
					nroFilaIter++;
					if (nroRegUnidadServ < cantRegistrosUnidadesServicios) filaExcelIter = hojaExcelSrv.CopyRow(nroFilaIter - 1, nroFilaIter);
				}
				//limpiar hoja si no hubieron registros
				
				fileName = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(fileName);
				string nombreRutaCarpeta = Path.GetDirectoryName(fileName);
				Directory.CreateDirectory(nombreRutaCarpeta);
				using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
				{
					PlantillaXltxWb.Write(fileStream);
					fileStream.Close();
					RestaurarLibro();
				}
        DejarLogRegistroEscrituraCompletaExcel(nroRegUnidadServ, registroServIter);
			}
		}

    public override void TerminarGeneracionReporte()
    {
    }

    // "Heredado" (entre comillas pues es método estático)
    //private static void LimpiarHoja(int nroRegUnidadServ, long servicioId, string nombreServicio)
    //{
    //	if (nroRegUnidadServ > 1)
    //	{
    //		string msjUltimaFilaExcel = String.Format("Ultimo nro de fila en Archivo Excel antes de borrar filas desde fila {0} (parte desde 0): {1}", nroFilaInicial + 1, hojaExcel.LastRowNum + 1);
    //		Console.WriteLine(msjUltimaFilaExcel);
    //		Log.Information(msjUltimaFilaExcel);
    //		int cantFilasEliminadas = ExcelHelper.MoverUltimaFilaHastaIndice(hojaExcel, nroFilaInicial);
    //		string msjCantFilasEliminadas = String.Format("Eliminadas {0} filas de Excel desde fila {1} (parte desde 1) para Servicio \"{2}\" (id={3})\ndespues de rellenar con {4} filas"
    //			, cantFilasEliminadas
    //			, nroFilaInicial + 1
    //			, nombreServicio, servicioId, nroRegUnidadServ);
    //		Console.WriteLine(msjCantFilasEliminadas);
    //		Log.Information(msjCantFilasEliminadas);
    //		msjUltimaFilaExcel = String.Format("Ultimo nro de fila en Archivo Excel DESPUES de borrar filas (parte desde 1): {0}", hojaExcel.LastRowNum + 1);
    //		Console.WriteLine(msjUltimaFilaExcel);
    //		Log.Information(msjUltimaFilaExcel);
    //	}
    //	for (int i = 0; i < cantCamposHoja; i++)
    //	{
    //		hojaExcel.GetRow(nroFilaInicial).GetCell(nroColumnaInicial + i).SetCellType(CellType.Blank);
    //	}
    //}
  }
}
