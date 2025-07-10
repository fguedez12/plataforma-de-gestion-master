using OrquestadorGesp.ContextEFNetCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using OrquestadorGesp.AppSettingsJson;
using NPOI.XSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using OrquestadorGesp.Helpers;
using Serilog;
using System.Collections.Generic;

namespace OrquestadorGesp.Reportes
{
	public class ReporteExtendido : ReporteBase
	{
		public List<ComprasReporteExtendido> registrosReporteExtendidoSP = new List<ComprasReporteExtendido>();
		private Type[] tipoPropiedad = null;
		private string[] nombresCamposDesdeSPHaciaExcel = null;
		private int[] casosTiposCamposDesdeSPHaciaExcel = null;
		private bool[] campoDefaultSinInfo = null;
		private bool[] campoDefaultEspecial = null;
		private bool[] campoUnidadMedida = null;
		private bool[] campoTipoTransaccion = null;
		private bool[] campoFechaFin = null;
		private string plantillaComentarioProrrateo = "de {0:dd/MM/yyyy} a {1:dd/MM/yyyy}: {2} dias = {3:0.00} kWh consumo total y {4:0.00} kWh diario para el periodo";
		private string comentarioProrrateo = string.Empty;
		private DateTime valorFechaIniIter = DateTime.MinValue, valorFechaFinIter = DateTime.MinValue;
		private float costoIter = 0.0f;
		private float consumoIter = 0.0f;
		private ICellStyle estiloCeldaProrrateo = null;
		private ICellStyle estiloCeldaFechaProrrateo = null;
		private _BasePkServicioEntity entityPkServicioBaseIter = null;
		private readonly DateTime FECHA_HOY = DateTime.Now.Date;

		public ReporteExtendido() : base(false)
		{
			LogicaComunConstructor();
		}
		public ReporteExtendido(bool borrarListaServicios = false) : base(borrarListaServicios)
		{
			LogicaComunConstructor();
		}

    public override void TerminarGeneracionReporte() { }

    private void LogicaComunConstructor()
		{
			//dbContextInstance = GespDbContext.GetInstance();
			anhoMinimoInicioProrateo = CurrentConfJson.CurrentConf.AnhoMinimoTantoCompraComoinicioLectura;
			tipoPropiedad = new Type[configReporteExtendido.ColumnasNombresCamposExcel.Length];
			casosTiposCamposDesdeSPHaciaExcel = new int[tipoPropiedad.Length];
			campoDefaultSinInfo = new bool[tipoPropiedad.Length];
			campoDefaultEspecial = new bool[tipoPropiedad.Length];
			campoUnidadMedida = new bool[tipoPropiedad.Length];
			campoTipoTransaccion = new bool[tipoPropiedad.Length];
			campoFechaFin = new bool[tipoPropiedad.Length];
			nombresCamposDesdeSPHaciaExcel = configReporteExtendido.ColumnasNombresCamposExcel;
			for (int i = 0; i < tipoPropiedad.Length; i++)
			{
				string nombrePropiedad = nombresCamposDesdeSPHaciaExcel[i];
				tipoPropiedad[i] = GeneralHelper.ObtenerTipoPropiedad(typeof(ComprasReporteExtendido), nombrePropiedad);
				campoDefaultSinInfo[i] = configReporteExtendido.ColumnasNombresCamposExcelConDefaultSinInfo.Contains(nombrePropiedad);
				campoDefaultEspecial[i] = configReporteExtendido.ColumnasNombresCamposExcelConDefaultEspecial.Contains(nombrePropiedad);
				campoUnidadMedida[i] = configReporteExtendido.NombreCampoUnidadMedida.Equals(nombrePropiedad);
				campoTipoTransaccion[i] = configReporteExtendido.NombreCampoTipoTransaccion.Equals(nombrePropiedad);
				campoFechaFin[i] = configReporteExtendido.NombreCampoFinLectura.Equals(nombrePropiedad);
				int valorTipoPropiedadCampoRegistro = 0;
				valorTipoPropiedadCampoRegistro += Convert.ToInt32(GeneralHelper.EsNumericoEntero(tipoPropiedad[i])) * 1;
				valorTipoPropiedadCampoRegistro += Convert.ToInt32(GeneralHelper.EsNumericoDecimal(tipoPropiedad[i])) * 2;
				valorTipoPropiedadCampoRegistro += Convert.ToInt32(GeneralHelper.EsBooleano(tipoPropiedad[i])) * 4;
				valorTipoPropiedadCampoRegistro += Convert.ToInt32(GeneralHelper.EsFechaHora(tipoPropiedad[i])) * 8;
				casosTiposCamposDesdeSPHaciaExcel[i] = valorTipoPropiedadCampoRegistro;
			}

		}

		private ConfReporteExtendido configReporteExtendido;

		private readonly string[] valoresDefaultNormal = new string[3] { "", "-1", null };
		private readonly string[] valoresDefaultEspecial = new string[2] { "", null };
		//private readonly string formatoLineaStr = "Info reg. nro {0} de {1} para fila {2} e IdServicio {3}. Es encabezado? {4}. IdCompra: {5}, IdCompraMedidor: {6}";
		private int anhoMinimoInicioProrateo = int.MaxValue;
		//private readonly string formatoColumnaStr = "Propiedad {0} = [[{1}]]";
		//private readonly string formatoCeldaStr = "Celda {0}{1} es Nula? {2}";
		private int nroFilaIter = 0;
		private int nroRegReporteExtparaSrv = 0;

		enum CasoValorExcel
		{
			EsCampoNormal = 1,
			EsCampoDefaultSinInfo = 2,
			EsCampoDefaultEspecialSinInfo = 4,
			EsCampoTransaccionProrrateado = 8,
			EsCampoUnidadMedida = 16
		}
		public override void ObtenerConfReporte()
		{
			ConfReporte = CurrConfGlobal.ReporteExtendido;
			configReporteExtendido = (ConfReporteExtendido)ConfReporte;
		}

		public override void ObtenerDatosSP()
		{
			if (entityPkServicioBaseIter == null) return;
			var commandStr = string.Format(PLANTILLA_COD_SQL_EJECUTAR_SP_SIN_PARAMS, string.Format(nombreSP, entityPkServicioBaseIter.Int64PK));
			var msjCommandStr = string.Format("<{0}>. Comando SQL A Ejecutar=\"{1}\"", GetType().Name, commandStr);
			Console.WriteLine(msjCommandStr);
			Log.Information(msjCommandStr);
			// FALLA:
			// falla obtencion de datos de SP SP_REPORTE_EXTENDIDO porque esta mapeado una propiedad que no puede ser nula y viene nula
			registrosReporteExtendidoSP = DbContextInstance.ComprasReporteExtendidoSet.FromSql(commandStr).ToList();
		}


		public override void ObtenerDatosCSV()
		{
		}

		public override void ObtenerReporte()
		{
			try
			{

				//if (registrosReporteExtendidoSP == null)
				//if (entityPkServicioBaseIter == null)
				//{
				//	var mensajeNoHayRegistrosFaltaInvocacionInicial = "Este metodo se debe invocar cuando se tenga el registro de iteracion de servicio entityPkServicioBaseIter inicializado" +
				//		"\ny luego invocar por cada Servicio el método ObtenerDatosSP";
				//	var mensajeSaliendoMetodo = "Saliendo de ReporteUnidadesPorCadaServicio.ObtenerReporte()";
				//	Console.WriteLine(mensajeNoHayRegistrosFaltaInvocacionInicial);
				//	Console.WriteLine(mensajeSaliendoMetodo);
				//	Log.Warning(mensajeNoHayRegistrosFaltaInvocacionInicial);
				//	Log.Warning(mensajeSaliendoMetodo);
				//	return;
				//}
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
					GeneralHelper.RemoveLastChar(rutaPlantillasReportes) : rutaPlantillasReportes;
				string subRutaPlantillaReporteExtPorServ = subRutaPlantillaReporte.EndsWith(FileSystemHelper.DIR_SEPARATOR) ?
					GeneralHelper.RemoveLastChar(subRutaPlantillaReporte) : subRutaPlantillaReporte;
				rutaPlantillaReporte = String.Format("{0}{1}{2}{1}{3}"
					, rutaPlantillaReporte
					, FileSystemHelper.DIR_SEPARATOR
					, subRutaPlantillaReporte
					, archivoPlantillaReporte);
				var REPORTE_CONF_EXTENDIDO = (ConfReporteExtendido)ConfReporte;
				string columnaInicialIteracion = ConfReporte.ColumnaInicialExcelIteracionDatos;
				string nombreCampoIdServicio = REPORTE_CONF_EXTENDIDO.NombreCampoIdServicio;
				string formatoFechaDisplay = CurrConfGlobal.FormatoFechaDisplay;
				string extensionArchivoReporte = rutaPlantillaReporte.Substring(
					rutaPlantillaReporte.LastIndexOf('.') + 1
				);
				string rutaCarpetaRaizDestino = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(string.Format("{0}{1}", baseDir, subRutaPlantillaReporte));
				Directory.CreateDirectory(rutaCarpetaRaizDestino);
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
				//formatoFechaDDMMSSSSConGuion = (XSSFDataFormat)plantillaXltxWb.CreateDataFormat();
				//estiloFechaDDMMSSSSConGuion = (XSSFCellStyle)plantillaXltxWb.CreateCellStyle();

				//estiloFechaDDMMSSSSConGuion.DataFormat = formatoFechaDDMMSSSSConGuion.GetFormat(ConfReporteBase.FORMATO_SOLO_FECHA_CON_GUION);
				//estiloFechaDDMMSSSSConGuion.SetDataFormat(estiloFechaDDMMSSSSConGuion.DataFormat);
				//cellStyle.setDataFormat(creationHelper.createDataFormat().getFormat("dddd dd/mm/yyyy"));
				nroColumnaInicial = columnaInicial - 'A';
				var registrosServicios = ListaServiciosBase;
				//= registrosReporteExtendidoSP.GroupBy(s => s.ServicioId);

				string datosInicialesStr = String.Format("Cantidad de servicios encontrados:{0}\nColumna Inicial: {1}={2}\nFila inicial iteracion: {3}"
					, registrosServicios.Count()
					, columnaInicial
					, nroColumnaInicial
					, nroFilaInicial);

				//LimpiarHoja(0, nroFilaInicial, "<Limpieza Inicial Hoja Excel>");
				Console.WriteLine(datosInicialesStr);
				Log.Information(datosInicialesStr);

				var reporteExtendidoConf = (ConfReporteExtendido)ConfReporte;
				//var mensajeInstanciadoCantRegistrosTotal = string.Format("Cantidad total de registros de {0}={1}", GetType().Name, registrosReporteExtendidoSP.Count());
				//Console.WriteLine(mensajeInstanciadoCantRegistrosTotal);
				//Log.Information(mensajeInstanciadoCantRegistrosTotal);
				//List<DateTime> fechasUltimoDiaCadaMesAnho = new List<DateTime>();
				//string formatoLineaStr = "Info reg. nro {0} de {1}";
				//string formatoColumnaStr = "Propiedad {0} = [[{1}]]";
				//string formatoCeldaStr = "Celda {0}{1} es Nula? {2}";
				RespaldarLibro();

				foreach (var registroServIter in registrosServicios)
				{
					try {
						formatoDecimal = PlantillaXltxWb.CreateDataFormat().GetFormat("0.00");
						formatoFecha = PlantillaXltxWb.CreateDataFormat().GetFormat(ConfReporteBase.FORMATO_SOLO_FECHA_MINUCULA);
						estiloCeldaProrrateo = PlantillaXltxWb.CreateCellStyle();
						estiloCeldaFechaProrrateo = PlantillaXltxWb.CreateCellStyle();
						IFont font = PlantillaXltxWb.CreateFont();
						font.Color = IndexedColors.Brown.Index;
						estiloCeldaProrrateo.SetFont(font);
						estiloCeldaFechaProrrateo.SetFont(font); //wb.getCreationHelper().createDataFormat().getFormat("HH:mm:ss")

						HojaExcel = (XSSFSheet)PlantillaXltxWb.GetSheetAt(0);
						entityPkServicioBaseIter = registroServIter;
						
						ObtenerDatosSP();

						List<ComprasReporteExtendido> conjuntoSiguientesCompras = new List<ComprasReporteExtendido>();
						nroRegReporteExtparaSrv = 0;
						nroFilaIter = nroFilaInicial;
						//var registrosReporteExtendidoServ = registrosReporteExtendidoSP
						//	.Where(r => r.ServicioId == registroServIter.Int64PK).ToList();

						int cantRegsReporteExtendidoParaServ = registrosReporteExtendidoSP.Count;
						//var mensajeInstanciadoRegistrosServicioIter = string.Format("Cantidad total de registros de {0} para Servicio {1} (id={2})={3}, filaIter:{4}, filaInicial:{5}"
						//	, GetType().Name
						//	, registroServIter.NombreServicio
						//	, registroServIter.Int64PK
						//	, cantRegsReporteExtendidoParaServ
						//	, nroFilaIter
						//	, nroFilaInicial);
						//Console.WriteLine(mensajeInstanciadoRegistrosServicioIter);
						//Log.Information(mensajeInstanciadoRegistrosServicioIter);

						//if (HojaExcel.DrawingPatriarch == null) HojaExcel.CreateDrawingPatriarch();

						string fileName = string.Format("{0}{1}{2}.{3}", rutaCarpetaRaizDestino, FileSystemHelper.DIR_SEPARATOR, registroServIter.Int64PK, extensionArchivoReporte);
						//LimpiarHoja(0, registroServIter.Int64PK, "<Limpieza Archivo Excel Para Servicio>");
						if (cantRegsReporteExtendidoParaServ > 0)
						{
              registrosReporteExtendidoSP
              .OrderBy(r => r.RegionOrder)
							.ThenBy(r => r.Comuna)
							.ThenBy(r => r.IdDivisionCompra)
							.ThenBy(r => r.IdCompra)
							.ThenBy(r => r.IdCompraMedidor)
							.ThenBy(r => r.Energetico);
							//int nroRegistroReporte = 0;

							foreach (var regCompraConsumo in registrosReporteExtendidoSP)
							{
								try
								{
									float factor = regCompraConsumo.Factor == null ? -1.0f : (float)regCompraConsumo.Factor;
									float cantidadEncabezado = (float)regCompraConsumo.Cantidad;
									DateTime fechaDesde;
									DateTime fechaHasta;
									ComprasReporteExtendido siguienteRegCompraConsumo;
									//string mensajeFilaProrrateo = string.Format("Encabezado prorrateo. Fila inicial={0}. " +
									//	"Energetico permite medidor={1}, Enegetico=\"{2}\"(idEnerg={3}), Nro cliente nulo={4}, " +
									//	"Factor={5}, Cantidad={6}, Region={7}, RegionOrder={8}, " +
									//	"IdDivisionCompra={9} IdCompra={10}, IdCompraMedidor={11}"
									//	, nroFilaIter
									//	, regCompraConsumo.EnergeticoPermiteMedidor
									//	, regCompraConsumo.Energetico
									//	, regCompraConsumo.EnergeticoId
									//	, regCompraConsumo.IdNumeroDeCliente < 0
									//	, regCompraConsumo.Factor
									//	, regCompraConsumo.Cantidad
									//	, regCompraConsumo.Region
									//	, regCompraConsumo.RegionOrder
									//	, regCompraConsumo.IdDivisionCompra
									//	, regCompraConsumo.IdCompra
									//	, regCompraConsumo.IdCompraMedidor
									//	);
									if (regCompraConsumo.EnergeticoPermiteMedidor)
									{
										if (regCompraConsumo.IdNumeroDeCliente < 0) //GLP sin Inicio ni fin de lectura
										{
											fechaDesde = regCompraConsumo.FechaCompra.Date;
											siguienteRegCompraConsumo = null;
                      var conjuntoSiguientesComprasEnumSinNroCli = registrosReporteExtendidoSP.Where(s => s.IdDivisionCompra == regCompraConsumo.IdDivisionCompra);
                      conjuntoSiguientesComprasEnumSinNroCli = conjuntoSiguientesCompras.Where(e => e.EnergeticoId == regCompraConsumo.EnergeticoId);
                      conjuntoSiguientesComprasEnumSinNroCli = conjuntoSiguientesCompras.Where(n => n.IdNumeroDeCliente == regCompraConsumo.IdNumeroDeCliente);
                      conjuntoSiguientesComprasEnumSinNroCli = conjuntoSiguientesCompras.Where(f => f.FechaCompra > regCompraConsumo.FechaCompra);
                      conjuntoSiguientesComprasEnumSinNroCli = conjuntoSiguientesCompras.Where(c => c.IdCompra != regCompraConsumo.IdCompra);
                      conjuntoSiguientesCompras.Clear();
                      conjuntoSiguientesCompras.AddRange(conjuntoSiguientesComprasEnumSinNroCli);

           //           conjuntoSiguientesCompras = registrosReporteExtendidoSP.Where(s => s.IdDivisionCompra == regCompraConsumo.IdDivisionCompra);
											//conjuntoSiguientesCompras = conjuntoSiguientesCompras.Where(e => e.EnergeticoId == regCompraConsumo.EnergeticoId);
											//conjuntoSiguientesCompras = conjuntoSiguientesCompras.Where(n => n.IdNumeroDeCliente == regCompraConsumo.IdNumeroDeCliente);
											//conjuntoSiguientesCompras = conjuntoSiguientesCompras.Where(f => f.FechaCompra > regCompraConsumo.FechaCompra);
											//conjuntoSiguientesCompras = conjuntoSiguientesCompras.Where(c => c.IdCompra != regCompraConsumo.IdCompra);
											//conjuntoSiguientesCompras = registrosReporteExtendidoServ
											//	.Where(r => r.IdCompra != regCompraConsumo.IdCompra
											//	&& r.IdDivisionCompra == regCompraConsumo.IdDivisionCompra
											//	&& r.EnergeticoId == regCompraConsumo.EnergeticoId
											//	&& r.IdNumeroDeCliente < 0);
											if (conjuntoSiguientesCompras.Count > 0)
											{
												conjuntoSiguientesCompras.OrderBy(c => c.FechaCompra);
												siguienteRegCompraConsumo = conjuntoSiguientesCompras.FirstOrDefault();
											}
											//siguienteRegCompraConsumo = registrosReporteExtendidoServ
											//	.Where(s => s.IdDivisionCompra == regCompraConsumo.IdDivisionCompra)
											//	.Where(s => s.EnergeticoId == regCompraConsumo.EnergeticoId)
											//	.Where(s => s.IdCompraMedidor == reporteExtendidoConf.IdCompraMedidorSinMedidor)
											//	.OrderByDescending(s => s.FechaCompra).FirstOrDefault(null);
											fechaHasta = FECHA_HOY;
											if (siguienteRegCompraConsumo != null)
											{
												fechaHasta = siguienteRegCompraConsumo.FechaCompra.Date;
												//string mensajeFilaCalculoProrrateoSinIdNroCliente = string.Format("Enegetico=\"{0}\"(idEnerg={1}) [GLP debiese ser], Fecha Compra Desde:{2} Fecha Siguiente CompraConsumo:{3}"
												//	, regCompraConsumo.Energetico
												//	, regCompraConsumo.EnergeticoId
												//	, fechaDesde.ToString(ConfReporteBase.FORMATO_SOLO_FECHA)
												//	, fechaHasta.ToString(ConfReporteBase.FORMATO_SOLO_FECHA));
												//Console.WriteLine(mensajeFilaCalculoProrrateoSinIdNroCliente);
												//Log.Information(mensajeFilaCalculoProrrateoSinIdNroCliente);
											}
											RellenarExcelConProrrateo(cantRegsReporteExtendidoParaServ
												, regCompraConsumo
												, factor
												, cantidadEncabezado
												, fechaDesde
												, fechaHasta);
											continue;
										}
										//GLP CON MEDIDOR o cualquier ENERGETICO que PERMITA MEDIDOR
										DateTime fechaLecDesde = regCompraConsumo.InicioLectura;
										DateTime fechaLecHasta = regCompraConsumo.FinLectura;
										//string mensajeFilaCalculoProrrateoConMedidorMasIdNroCliente = string.Format("Enegetico=\"{0}\"(idEnerg={1}), Fecha Lec. Desde:{2} Fecha Lec. Hasta:{3}"
										//	, regCompraConsumo.Energetico
										//	, regCompraConsumo.EnergeticoId
										//	, fechaLecDesde.ToString(ConfReporteBase.FORMATO_SOLO_FECHA)
										//	, fechaLecHasta.ToString(ConfReporteBase.FORMATO_SOLO_FECHA));
										//Console.WriteLine(mensajeFilaCalculoProrrateoConMedidorMasIdNroCliente);
										//Log.Information(mensajeFilaCalculoProrrateoConMedidorMasIdNroCliente);

										RellenarExcelConProrrateo(cantRegsReporteExtendidoParaServ
											, regCompraConsumo
											, factor
											, cantidadEncabezado
											, fechaLecDesde
											, fechaLecHasta);
										nroRegReporteExtparaSrv++;
										continue;
									}
									// Los energeticos que NO PERMITEN medidor
									fechaDesde = regCompraConsumo.FechaCompra.Date;
									siguienteRegCompraConsumo = null;
									var conjuntoSiguientesComprasEnumSinMed = registrosReporteExtendidoSP.Where(s => s.IdDivisionCompra == regCompraConsumo.IdDivisionCompra);
									conjuntoSiguientesComprasEnumSinMed = conjuntoSiguientesCompras.Where(e => e.EnergeticoId == regCompraConsumo.EnergeticoId);
									conjuntoSiguientesComprasEnumSinMed = conjuntoSiguientesCompras.Where(n => n.IdNumeroDeCliente == regCompraConsumo.IdNumeroDeCliente);
									conjuntoSiguientesComprasEnumSinMed = conjuntoSiguientesCompras.Where(f => f.FechaCompra > regCompraConsumo.FechaCompra);
                  conjuntoSiguientesComprasEnumSinMed = conjuntoSiguientesCompras.Where(c => c.IdCompra != regCompraConsumo.IdCompra);
                  conjuntoSiguientesCompras.Clear();
                  conjuntoSiguientesCompras.AddRange(conjuntoSiguientesComprasEnumSinMed);
                  if (conjuntoSiguientesCompras.Count > 0)
									{
										conjuntoSiguientesCompras.OrderBy(c => c.FechaCompra);
										siguienteRegCompraConsumo = conjuntoSiguientesCompras.FirstOrDefault();
									}

									//.Where(s => s.IdDivisionCompra == regCompraConsumo.IdDivisionCompra)
									//.Where(s => s.EnergeticoId == regCompraConsumo.EnergeticoId)
									//.Where(s => s.IdCompraMedidor == regCompraConsumo.IdCompraMedidor)
									//.OrderByDescending(s => s.FechaCompra).FirstOrDefault(null);
									fechaHasta = FECHA_HOY;
									if (siguienteRegCompraConsumo != null)
									{
										fechaHasta = siguienteRegCompraConsumo.FechaCompra.Date;
										//string mensajeFilaCalculoProrrateoNoPermiteMedidor = string.Format("Enegetico=\"{0}\"(idEnerg={1}) [No permite medidor], Fecha Compra Del:{2} A siguiente compra el:{3}"
										//	, regCompraConsumo.Energetico
										//	, regCompraConsumo.EnergeticoId
										//	, fechaDesde.ToString(ConfReporteBase.FORMATO_SOLO_FECHA)
										//	, fechaHasta.ToString(ConfReporteBase.FORMATO_SOLO_FECHA));
										//Console.WriteLine(mensajeFilaCalculoProrrateoNoPermiteMedidor);
										//Log.Information(mensajeFilaCalculoProrrateoNoPermiteMedidor);
									}

									RellenarExcelConProrrateo(cantRegsReporteExtendidoParaServ
										, regCompraConsumo
										, factor
										, cantidadEncabezado
										, fechaDesde
										, fechaHasta);
									
								} catch (Exception exProrrateo)
								{
									var mensajeplantillaExProrrateo = "Excepcion al prorratear IdCompra={0}, " +
										"IdCompraMedidor={1}. IdDivisionCompra={2}, " +
									 "Cantidad=[{3:0.00}], Factor:[{4:0.00}], Energetico(nombre,id)=({5}{6}), " +
									 "FechaCompra:{7:dd/MM/yyyy}, InicioLectura:{8:dd/MM/yyyy}, FinLectura:{9:dd/MM/yyyy}, TipoExcepcion:{10}";
									Console.WriteLine(mensajeplantillaExProrrateo
									 , regCompraConsumo.IdCompra, regCompraConsumo.IdCompraMedidor, regCompraConsumo.IdDivisionCompra
									 , regCompraConsumo.Cantidad, regCompraConsumo.Factor, regCompraConsumo.Energetico, regCompraConsumo.EnergeticoId
									 , regCompraConsumo.FechaCompra, regCompraConsumo.InicioLectura, regCompraConsumo.FinLectura, exProrrateo.GetType().AssemblyQualifiedName, exProrrateo
									 );
									Log.Warning(mensajeplantillaExProrrateo
									 , regCompraConsumo.IdCompra, regCompraConsumo.IdCompraMedidor, regCompraConsumo.IdDivisionCompra
									 , regCompraConsumo.Cantidad, regCompraConsumo.Factor, regCompraConsumo.Energetico, regCompraConsumo.EnergeticoId
									 , regCompraConsumo.FechaCompra, regCompraConsumo.InicioLectura, regCompraConsumo.FinLectura, exProrrateo.GetType().AssemblyQualifiedName, exProrrateo
									 );
									Log.Warning(exProrrateo.Message + ", TRAZA:\n {0}", exProrrateo.StackTrace, exProrrateo);
								}
							}

							fileName = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(fileName);
              FileInfo fileInfo = new FileInfo(fileName);
              using (FileStream fileStream = fileInfo.Open(FileMode.Create, FileAccess.Write, FileShare.None))
              {
                //PlantillaXltxWb.Write(fileStream, false);
                PlantillaXltxWb.Write(fileStream);
                long length = fileStream.Length;
                fileStream.Close();
                foreach (string subRutaInsumo in configReporteExtendido.SubRutasParaInsumo)
                {
                  string rutaDestinoInsumoIter = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(Path.GetDirectoryName(fileName) + FileSystemHelper.DIR_SEPARATOR + subRutaInsumo);
                  Directory.CreateDirectory(rutaDestinoInsumoIter);
                  rutaDestinoInsumoIter += FileSystemHelper.DIR_SEPARATOR + Path.GetFileName(fileName);
                  rutaDestinoInsumoIter = FileSystemHelper.LimpiarSeparadoresDirConsecutivos(rutaDestinoInsumoIter);
                  File.Copy(fileName, rutaDestinoInsumoIter);
                }
                //fileInfo.Attributes = FileAttributes.Normal;
                //fileStream.Unlock(0, length);
                RestaurarLibro();
							}

              //LimpiarHoja(cantRegsReporteExtendidoParaServ, registroServIter.Int64PK, registroServIter.NombreServicio);
              DejarLogRegistroEscrituraCompletaExcel(cantRegsReporteExtendidoParaServ, registroServIter);
						} 
					} catch (Exception exLoop)
					{
						var mensajePlantillaExLoopSrv = "Excepcion al procesar compras de Servicio IdServicio={0}, NombreServicio=[{1}], AnexoId:{2} "
							+ "TipoExcepcion={3}";
						Console.WriteLine(mensajePlantillaExLoopSrv, registroServIter.Int64PK, registroServIter.NombreServicio, registroServIter.AnexoId
							, exLoop.GetType().AssemblyQualifiedName, exLoop
							);
						Log.Error("Excepcion al procesar compras de Servicio IdServicio={0}, NombreServicio=[{1}], AnexoId:{2} "
							+ "TipoExcepcion={3}"
							, registroServIter.Int64PK, registroServIter.NombreServicio, registroServIter.AnexoId
							, exLoop.GetType().AssemblyQualifiedName, exLoop
						 );
						Log.Error(exLoop.Message+ ", TRAZA:\n {0}", exLoop.StackTrace, exLoop);
					}
				}
			}
			catch (Exception ex)
			{
				Console.Write("{0}:{1}:\n{2}:", ex.GetType().AssemblyQualifiedName, ex.Message, ex.StackTrace);
				Log.Error("{0}:{1}:\n{2}:", ex.GetType().AssemblyQualifiedName, ex.Message, ex.StackTrace);
				Log.Fatal("Excepcion Fatal!", ex);
			}
		}

		private bool validarRangoFechaProrrateo(DateTime fechaInicial, DateTime fechaFinal)
		{
			if (fechaInicial.Year < anhoMinimoInicioProrateo) return false;
			if (fechaFinal.Year < anhoMinimoInicioProrateo) return false;
			return true;
		}

		private void RellenarExcelConProrrateo(int nroRegReporteExtendidoParaServ
			, ComprasReporteExtendido regCompraConsumo
			, float factor
			, float cantidadEncabezado
			, DateTime fechaDesde
			, DateTime fechaHasta)
		{
			DateTime fecIterIni = fechaDesde.Date;
			DateTime fecIterFin = fechaDesde.Date;
			float kWhIter = 0.0f;
			float kWhPeriodoTotal = Math.Abs(cantidadEncabezado * factor);
			int cantDiasHastaFechaHasta = -1;
			int cantDiasTotales = -1;
			var rangoFechaOk = validarRangoFechaProrrateo(fechaDesde, fechaHasta);
			regCompraConsumo.FechaCompra = fechaDesde;
			float cantKWhPorDia = 0;
			float costoPorDia = 0;
			int cantDiasHastaPrimeroMesSgte = 0;
			string comentarioCabeceraProrrateo = string.Empty;
			//string formatoMensaje = "Cantidad en KWh desde el {0:dd/MM/yyyy} al {1:dd/MM/yyyy} ({2} dias) de \"{3}\": {4} kWh";
			if (rangoFechaOk)
			{
				cantDiasTotales = (fechaHasta - fechaDesde).Days;
				cantKWhPorDia = kWhPeriodoTotal / Convert.ToSingle(cantDiasTotales);
				costoPorDia = Convert.ToSingle(regCompraConsumo.Costo) / Convert.ToSingle(cantDiasTotales);
				cantDiasHastaFechaHasta = cantDiasTotales;
				//nroFilaIter = nroFilaInicial;
				//comentarioCabeceraProrrateo = string.Format(formatoMensaje, fechaDesde, fechaHasta, cantDiasTotales, regCompraConsumo.Energetico, kWhPeriodoTotal);
				//Console.WriteLine(comentarioCabeceraProrrateo);
				//Log.Debug(comentarioCabeceraProrrateo);
				//cantDiasHastaFechaHasta


				regCompraConsumo.Cantidad = cantidadEncabezado;
				//Console.WriteLine(string.Format("Cantidad  {0:0.00}, Factor: {1:0.00}, kWhPorDia: {2:0.00}", cantidadEncabezado, factor, cantKWhPorDia));
				EscribirFilaExcel(true
													, nroRegReporteExtendidoParaServ
													, regCompraConsumo);
			} else
			{
				//formatoMensaje = "Cantidad en KWh desde el {0:dd/MM/yyyy} al {1:dd/MM/yyyy} ({2} dias) de \"{3}\": {4} kWh. Rango de fecha invalido";

				regCompraConsumo.Cantidad = cantidadEncabezado;
				//comentarioCabeceraProrrateo = string.Format(formatoMensaje, fechaDesde, fechaHasta, cantDiasTotales, regCompraConsumo.Energetico, kWhPeriodoTotal);
				//Console.WriteLine(string.Format("Cantidad  {0:0.00}, Factor: {1:0.00}, kWhPorDia: {2:0.00}", cantidadEncabezado, factor, cantKWhPorDia));
				EscribirFilaExcel(true
													, nroRegReporteExtendidoParaServ
													, regCompraConsumo);
				//Console.WriteLine(comentarioCabeceraProrrateo);
				//Log.Debug(comentarioCabeceraProrrateo);
				return;
			}
			//fila inicial

			//prorrateo
			while (cantDiasHastaFechaHasta > 0)
			{
				fecIterFin = fecIterFin.UltimoDiaDelMes();
				fecIterFin = fecIterFin > fechaHasta ? fechaHasta : fecIterFin;
				cantDiasHastaPrimeroMesSgte = (fecIterFin - fecIterIni).Days;
				valorFechaIniIter = fecIterIni;
				valorFechaFinIter = fecIterFin;
				kWhIter = cantKWhPorDia * cantDiasHastaPrimeroMesSgte;
				consumoIter = kWhIter;
				costoIter = costoPorDia * cantDiasHastaPrimeroMesSgte;
				//Console.WriteLine(formatoMensaje, fechaDesde, fechaHasta, cantDiasTotales, regCompraConsumo.Energetico, regCompraConsumo.Cantidad);
				//Log.Information(formatoMensaje, fechaDesde, fechaHasta, cantDiasTotales, regCompraConsumo.Energetico, regCompraConsumo.Cantidad);
				comentarioProrrateo = string.Format(plantillaComentarioProrrateo, fecIterIni, fecIterFin, cantDiasHastaPrimeroMesSgte, kWhIter, cantKWhPorDia);
				fecIterIni = fecIterFin.Date;
				fecIterFin = fecIterFin.AddDays(1);
				//valorFechaIniIter = fecIterIni;
				cantDiasHastaFechaHasta -= cantDiasHastaPrimeroMesSgte;
				regCompraConsumo.Cantidad = kWhIter;

				EscribirFilaExcel(false
													, nroRegReporteExtendidoParaServ
													, regCompraConsumo);
				//Thread.Sleep(CurrentConfJson.CurrentConf.TiempoReposoHebrasParaNoAhogarCPUMsec);
			}

		}

		private void SetearValorCelda(int index, bool esEncabezado, ComprasReporteExtendido registroRepExtParaFilaExcel, out object valor, out string valorStr)
		{
			string nombreCampo = nombresCamposDesdeSPHaciaExcel[index];
			var valorParaEscribirCelda = GeneralHelper.ObtenerValorPropiedad(registroRepExtParaFilaExcel, nombreCampo);
			bool casoNormal = esEncabezado && valorParaEscribirCelda != null;
			if (casoNormal)
			{
				valor = valorParaEscribirCelda;
				valorStr = Convert.ToString(valorParaEscribirCelda);
				return;
			}
			bool esCampoUnidadMedida = campoUnidadMedida[index] && !esEncabezado;
			bool esCampoConsumo = campoTipoTransaccion[index] && !esEncabezado;
			bool esCampoFinLecturaCompra = campoFechaFin[index] && esEncabezado;
			if (esCampoUnidadMedida)
			{
				valor = configReporteExtendido.NombreUnidadConversionDestino;
				valorStr = configReporteExtendido.NombreUnidadConversionDestino;
				return;
			}
			if (esCampoFinLecturaCompra)
			{
				valor = string.IsNullOrWhiteSpace(Convert.ToString(valorParaEscribirCelda)) ? FECHA_HOY : (DateTime)valorParaEscribirCelda;
				valorStr = ((DateTime)valor).ToString(ConfReporteBase.FORMATO_SOLO_FECHA);
				return;
			}
			if (esCampoConsumo)
			{
				valor = configReporteExtendido.ValorTipoTransaccionConsumo;
				valorStr = configReporteExtendido.ValorTipoTransaccionConsumo;
				return;
			}
			if (campoDefaultEspecial[index])
			{
				valor = configReporteExtendido.ValorDefectoEspecial;
				valorStr = configReporteExtendido.ValorDefectoEspecial;
				return;
			}
			if (valorParaEscribirCelda == null)
			{
				valor = ConfReporteExtendido.VALOR_CAMPO_SIN_INFO;
				valorStr = ConfReporteExtendido.VALOR_CAMPO_SIN_INFO;
			}
			valor = valorParaEscribirCelda;
			valorStr = Convert.ToString(valorParaEscribirCelda);
			return;
		}

		public void EscribirFilaExcel(bool esEncabezado
		, int cantRegistrosRepExtServ
		, ComprasReporteExtendido registroRepExtParaFilaExcel)
		{
			//Console.WriteLine(string.Format(formatoLineaStr, nroRegReporteExtparaSrv + 1, cantRegistrosRepExtServ, nroFilaIter, registroRepExtParaFilaExcel.ServicioId, esEncabezado, registroRepExtParaFilaExcel.IdCompra, registroRepExtParaFilaExcel.IdCompraMedidor));
			//Log.Debug(string.Format(formatoLineaStr, nroRegReporteExtparaSrv + 1, cantRegistrosRepExtServ, nroFilaIter, registroRepExtParaFilaExcel.ServicioId, esEncabezado, registroRepExtParaFilaExcel.IdCompra, registroRepExtParaFilaExcel.IdCompraMedidor));
			XSSFRow filaExcelIter;
			var hojaExcelSrv = HojaExcel;
			int tipoEncabezado = Convert.ToInt32(nroFilaIter == nroFilaInicial) * 1 
				+ Convert.ToInt32(esEncabezado) * 2; //3: es encabezado 1ra fila (no se copia fila), 2: es encabezado fila posterior a la primera, 0: no es encabezado
			switch (tipoEncabezado)
			{
				case 2:
					//es encabezado posterior a primer fila -> copiar primera fila a fila actual
					filaExcelIter = (XSSFRow)hojaExcelSrv.CopyRow(nroFilaInicial, nroFilaIter);
					//filaExcelIter = (XSSFRow)ExcelHelper.CopiarReemplazarFila(hojaExcel, nroFilaInicial, nroFilaIter);
					//hojaExcel.CopyRows(nroFilaInicial, nroFilaInicial, nroFilaIter, 1);
					break;
				case 0:
					//NO ES encabezado -> copiar fila anterior a fila actual
					filaExcelIter = (XSSFRow)hojaExcelSrv.CopyRow(nroFilaIter - 1, nroFilaIter);
					//filaExcelIter = (XSSFRow)ExcelHelper.CopiarReemplazarFila(hojaExcel, nroFilaIter - 1, nroFilaIter);
					break;
				// default: es Primera fila y por ende NO SE COPIA, se REFERENCIA
			}
			filaExcelIter = (XSSFRow)hojaExcelSrv.GetRow(nroFilaIter);
			for (int i = 0; i < nombresCamposDesdeSPHaciaExcel.Length; i++)
			{
				XSSFCell celdaIter = (XSSFCell)filaExcelIter.GetCell(nroColumnaInicial + i);
				object valorParaEscribirCelda;
				string valorParaEscribirCeldaStr;
				SetearValorCelda(i, esEncabezado, registroRepExtParaFilaExcel, out valorParaEscribirCelda, out valorParaEscribirCeldaStr);
				CellType tipoValorCeldaCode = CellType.Unknown;
				short estiloCeldaCode = 0;
				long valorLongCelda = -1;
				double valorDoubleCelda = -1.0;
				int casoTipoDesdeSP = casosTiposCamposDesdeSPHaciaExcel[i];
				DateTime valorDateTimeCelda = FECHA_HOY;
				if (valorParaEscribirCelda == null)
				{
					valorParaEscribirCeldaStr = string.Empty;
					casoTipoDesdeSP = 0;
				}
				switch (casoTipoDesdeSP) {
					case 1: //enteros
						tipoValorCeldaCode = CellType.Numeric;
						estiloCeldaCode = ExcelHelper.FORMATO_NPOI_SIN_DECIMALES;
						valorLongCelda = Convert.ToInt64(valorParaEscribirCelda);

						celdaIter.SetCellValue(valorLongCelda);
						celdaIter.CellStyle.DataFormat = estiloCeldaCode;
						celdaIter.SetCellType(tipoValorCeldaCode);
						//tipoValorCelda = typeof(long);
						break;
					case 2: //decimales
						tipoValorCeldaCode = CellType.Numeric;
						estiloCeldaCode = ExcelHelper.FORMATO_NPOI_DOS_DECIMALES;
						valorDoubleCelda = Convert.ToDouble(valorParaEscribirCelda);
						celdaIter.CellStyle.DataFormat = estiloCeldaCode;
						celdaIter.SetCellType(tipoValorCeldaCode);
						celdaIter.SetCellValue(valorDoubleCelda);
						break;
					case 4: //bool
						tipoValorCeldaCode = CellType.String;
						estiloCeldaCode = ExcelHelper.FORMATO_NPOI_GENERAL;
						celdaIter.CellStyle.DataFormat = estiloCeldaCode;
						valorParaEscribirCeldaStr = Convert.ToBoolean(valorParaEscribirCelda) ? ConfReporteBase.SI : ConfReporteBase.NO;
						celdaIter.SetCellType(tipoValorCeldaCode);
						celdaIter.SetCellValue(valorParaEscribirCeldaStr);
						break;
					case 8: //fecha
						tipoValorCeldaCode = CellType.Numeric;
						//celdaIter.SetCellType(tipoValorCeldaCode);
						//celdaIter.CellStyle = estiloFechaDDMMSSSSConGuion;
						valorDateTimeCelda = (DateTime)valorParaEscribirCelda;
						//valorParaEscribirCeldaStr = valorDateTimeCelda.ToString(ConfReporteBase.FORMATO_SOLO_FECHA);
						celdaIter.SetCellType(tipoValorCeldaCode);
						celdaIter.SetCellValue(valorDateTimeCelda); 
						//celdaIter.SetCellValue(valorParaEscribirCeldaStr);
						celdaIter.CellStyle.DataFormat = formatoFecha;
						//byte[] color = new byte[] { 255, 224, 192 };
						//celdaIter.CellStyle.FillBackgroundColor = new XSSFColor(color).Index; //no se traduciría a color exacto
						break;
					default:
						tipoValorCeldaCode = CellType.String;
						estiloCeldaCode = ExcelHelper.FORMATO_NPOI_GENERAL;
						celdaIter.SetCellValue(valorParaEscribirCeldaStr);
						celdaIter.SetCellType(tipoValorCeldaCode);
						ICell cellNeeded = null;
						if (i - 6 >= 0 && campoTipoTransaccion[i-6] && !esEncabezado)
						{
							tipoValorCeldaCode = CellType.Numeric;
							cellNeeded = celdaIter.Row.GetCell(celdaIter.ColumnIndex);
							cellNeeded.SetCellValue(costoIter);
							cellNeeded.CellStyle = estiloCeldaProrrateo;
							cellNeeded.CellStyle.DataFormat = formatoDecimal;

							//cellNeeded = celdaIter.Row.GetCell(celdaIter.ColumnIndex - 2);
							//cellNeeded.SetCellValue(consumoIter);
							//cellNeeded.CellStyle = estiloCeldaProrrateo;

							cellNeeded = celdaIter.Row.GetCell(celdaIter.ColumnIndex - 3);
							cellNeeded.SetCellValue(valorFechaFinIter);
							cellNeeded.CellStyle = estiloCeldaFechaProrrateo;
							cellNeeded.CellStyle.DataFormat = formatoFecha;

							cellNeeded = celdaIter.Row.GetCell(celdaIter.ColumnIndex - 4);
							cellNeeded.SetCellValue(valorFechaIniIter);
							cellNeeded.CellStyle = estiloCeldaFechaProrrateo;
							cellNeeded.CellStyle.DataFormat = formatoFecha;
							//celdaIter.CellStyle.
							//IClientAnchor anchorComment = plantillaXltxWb.GetCreationHelper().CreateClientAnchor();
							//anchorComment.Col1 = celdaIter.ColumnIndex;
							//anchorComment.Col2 = celdaIter.ColumnIndex + 5;
							//anchorComment.Row1 = celdaIter.RowIndex;
							//anchorComment.Row2 = celdaIter.RowIndex;
							//IComment comentarioCelda = HojaExcel.DrawingPatriarch.CreateCellComment(anchorComment);
							//comentarioCelda.Author = GetType().Name;
							//comentarioCelda.String = new XSSFRichTextString(comentarioProrrateo);
							//celdaIter.CellComment = comentarioCelda;
						}
            cellNeeded = null;
            break;
				}
        //celdaIter = null;
        //valorParaEscribirCelda = null;
        //valorParaEscribirCeldaStr = null;
      }
      //filaExcelIter = null;
      nroFilaIter++;
		}
	}
}
