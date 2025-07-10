using OrquestadorGesp.ContextEFNetCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using OrquestadorGesp.AppSettingsJson;
using FastMember;
using System.Runtime.CompilerServices;
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
	public class ReporteUnidadesPorServicioConEficEnerg : ReporteBase
	{
		public List<UnidadesPorServicioConEficEnerg> registrosUnidadesPorServicioConEficEnergGlobal;

		public ReporteUnidadesPorServicioConEficEnerg() : base(false) { }
		public ReporteUnidadesPorServicioConEficEnerg(bool limpiarIdsServicios = false) : base( limpiarIdsServicios) { }
		public override void ObtenerConfReporte()
		{
			ConfReporte = CurrConfGlobal.ReporteUnidadesPorServicioConEficEnerg;
		}

		public override void ObtenerDatosSP()
		{
			var commandStr = string.Format(PLANTILLA_COD_SQL_EJECUTAR_SP_SIN_PARAMS, nombreSP);
			var msjCommandStr = string.Format("{0}. Comando SQL A Ejecutar: {1}", this.GetType().Name, commandStr);
			Console.WriteLine(msjCommandStr);
			Log.Information(msjCommandStr);
			registrosUnidadesPorServicioConEficEnergGlobal = dbContext.UnidadesPorServicioConEficEnergSet.FromSql(PLANTILLA_COD_SQL_EJECUTAR_SP_SIN_PARAMS, nombreSP).ToList();
		}

		public override void ObtenerDatosCSV()
		{

		}

		public override void ObtenerReporte()
		{
			if (registrosUnidadesPorServicioConEficEnergGlobal == null)
			{
				var mensajeNoHayRegistrosFaltaInvocacionInicial = "No hay registros para ReportePorServicioConEficEnerg o bien no se invoco metodo ObtenerDatosSP";
				var mensajeSaliendoMetodo = "Saliendo de ReportePorServicioConEficEnerg.ObtenerReporte()";
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
			var reporteConEfiEnergConf = (ConfReporteUnidadesPorServicioConEficEnerg)ConfReporte;
			string celdaFechaAnexo = reporteConEfiEnergConf.CeldaAnexoExcel;
			string celdaCantidadRegistros = reporteConEfiEnergConf.CeldaCantidadRegistros;
			string archivoPlantillaReporte = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(ConfReporte.ArchivoPlantillaExcel);
			string rutaPlantillaReporte = rutaPlantillasReportes.EndsWith(FileSystemHelper.DIR_SEPARATOR) ?
				GeneralHelper.RemoveLastChar(rutaPlantillasReportes) : rutaPlantillasReportes;
			subRutaPlantillaReporte = subRutaPlantillaReporte.EndsWith(FileSystemHelper.DIR_SEPARATOR) ?
				GeneralHelper.RemoveLastChar(subRutaPlantillaReporte) : subRutaPlantillaReporte;
			rutaPlantillaReporte = string.Format("{0}{1}{2}{1}{3}"
				, rutaPlantillaReporte
				, FileSystemHelper.DIR_SEPARATOR
				, subRutaPlantillaReporte
				, archivoPlantillaReporte);
			//string celdaFechaHoy = REPORTE_CONF.CeldaFechaHoyExcel;
			string celdaNombreServicio = reporteConEfiEnergConf.CeldaNombreServicioExcel;
			string columnaInicialIteracion = ConfReporte.ColumnaInicialExcelIteracionDatos;
			string nombreCampoNombreServicio = reporteConEfiEnergConf.NombreCampoNombreServicio;
			string nombreCampoIdServicio = reporteConEfiEnergConf.NombreCampoIdServicio;
			string[] nombresCamposDesdeSPHaciaExcel = ConfReporte.ColumnasNombresCamposExcel;
			string extensionArchivoReporte = rutaPlantillaReporte.Substring(
				rutaPlantillaReporte.LastIndexOf('.') + 1
			);
			string carpetaDestinoReportes = string.Format("{0}{1}", baseDir, subRutaPlantillaReporte);
			carpetaDestinoReportes = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(carpetaDestinoReportes);
			Directory.CreateDirectory(carpetaDestinoReportes);

			Console.WriteLine("Carpeta destino de {0}: {1}",GetType().Name ,carpetaDestinoReportes);
			Log.Information("Carpeta destino de {0}: {1}", GetType().Name, carpetaDestinoReportes);

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

			RespaldarLibro();
			foreach (var servicioIter in ListaServiciosBase)
			{
				Thread.Sleep(CurrentConfJson.CurrentConf.TiempoReposoHebrasParaNoAhogarCPUMsec);
				//	var registroServIter = registrosServicios.Select(s => s.IdServicio == ServicioId);
				HojaExcel = (XSSFSheet)PlantillaXltxWb.GetSheetAt(0);
				var hojaExcelSrv = HojaExcel;
				var registrosUnidadesServicios = registrosUnidadesPorServicioConEficEnergGlobal.Where(r => r.ServicioId == servicioIter.Int64PK).OrderBy(r => r.RegionPos).ThenBy(nc => nc.NombreComuna);
				var refCeldaExcel = new CellReference(celdaFechaAnexo);
				XSSFRow filaExcel = (XSSFRow)hojaExcelSrv.GetRow(refCeldaExcel.Row);
				XSSFCell celdaExcel = (XSSFCell)filaExcel.GetCell(refCeldaExcel.Col);
				
				string datosInicialesStr = String.Format("Cantidad de servicios encontrados:{0}\nColumna Inicial: {1}={2}\nFila inicial iteracion: {3}"
					, registrosUnidadesServicios.Count()
					, columnaInicial
					, nroColumnaInicial
					, nroFilaInicial);
				var registroServIter = registrosUnidadesServicios.FirstOrDefault();
				if (servicioIter.AnexoId > 0)
				{
					celdaExcel.SetCellType(CellType.Numeric);
					celdaExcel.SetCellValue(servicioIter.AnexoId);
				}
				else
				{
					celdaExcel.SetCellType(CellType.String);
					celdaExcel.SetCellValue(String.Empty);
				}
				refCeldaExcel = new CellReference(celdaCantidadRegistros);
				filaExcel = (XSSFRow)hojaExcelSrv.GetRow(refCeldaExcel.Row);
				celdaExcel = (XSSFCell)filaExcel.GetCell(refCeldaExcel.Col);
				celdaExcel.SetCellType(CellType.Numeric);
				celdaExcel.SetCellValue(registrosUnidadesServicios.Count());

				refCeldaExcel = new CellReference(celdaNombreServicio);
				filaExcel = (XSSFRow)hojaExcelSrv.GetRow(refCeldaExcel.Row);
				celdaExcel = (XSSFCell)filaExcel.GetCell(refCeldaExcel.Col);
				celdaExcel.SetCellValue(servicioIter.NombreServicio);
				string fileName = string.Format("{0}{1}{2}.{3}", carpetaDestinoReportes, FileSystemHelper.DIR_SEPARATOR, servicioIter.Int64PK, extensionArchivoReporte);
				XSSFRow filaExcelInicial = (XSSFRow)hojaExcelSrv.GetRow(nroFilaInicial);
				IRow filaExcelIter = filaExcelInicial;
				int nroFilaIter = nroFilaInicial;
				int cantRegistrosUnidadesServicios = registrosUnidadesServicios.Count();
				//string formatoLineaStr = "Info reg. nro {0} de {1}";
				//string formatoColumnaStr = "Propiedad {0} = [[{1}]]";
				//string formatoCeldaStr = "Celda {0}{1} es Nula? {2}";
				int nroRegUnidadServ = 0;
				foreach (var registroUnidadServicio in registrosUnidadesServicios)
				{
					Thread.Sleep(CurrentConfJson.CurrentConf.TiempoReposoHebrasParaNoAhogarCPUMsec);
					//Console.WriteLine(string.Format(formatoLineaStr, nroRegUnidadServ + 1, cantRegistrosUnidadesServicios));
					//Log.Information(string.Format(formatoLineaStr, nroRegUnidadServ + 1, cantRegistrosUnidadesServicios));
					for (int i = 0; i < nombresCamposDesdeSPHaciaExcel.Length; i++)
					{
						string nombreCampo = nombresCamposDesdeSPHaciaExcel[i];
						var valorParaEscribirCelda = GeneralHelper.ObtenerValorPropiedad(registroUnidadServicio, nombreCampo);
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
				using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
				{
					PlantillaXltxWb.Write(fileStream);
					fileStream.Close();
					RestaurarLibro();
				}
        DejarLogRegistroEscrituraCompletaExcel(nroRegUnidadServ, servicioIter);
			}
		}

    public override void TerminarGeneracionReporte()
    {
    }
  }
}
