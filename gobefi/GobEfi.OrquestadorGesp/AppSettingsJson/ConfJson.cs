namespace OrquestadorGesp.AppSettingsJson
{
	public class ConfJson
	{
		public ConfJson()
		{

		}
		public string DBConnName { get; set; }
		public string DBConnValue { get; set; }
		public string DBName { get; set; }
		public string FormatoFechaDisplay { get; set; }
		public string RutaPlantillasExcel { get; set; }
		public string QueryIdsTodosLosServicios { get; set; }
		public int DBTimeoutMsec { get; set; }
		public int TiempoReposoHebrasParaNoAhogarCPUMsec { get; set; }
		public int AnhoMinimoTantoCompraComoinicioLectura { get; set; }
		public string ExtensionArchivosExcel { get; set; }
		public ConfReporteUnidadesPorServicio ReporteUnidadesPorServicio { get; set; }
		public ConfReporteUnidadesPorServicioConEficEnerg ReporteUnidadesPorServicioConEficEnerg { get; set; }
		public ConfReporteExtendido ReporteExtendido { get; set; }
		public ConfReporteCompacto ReporteCompacto { get; set; }
    public ConfReporteControlDeCarga ReporteControlDeCarga { get; set; }
  }
}
