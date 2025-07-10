using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.AppSettingsJson
{
	public class ConfReporteConInsumoRepExt : ConfReporteBase
	{
		public string RutaRaizFisicaCarpetaDeRedUnidadRedEtcInsumoRepExt { get; set; }
		public Dictionary<string, string> ColumnasRepExtInsumo { get; set; }
		public int TiempoEsperaExistenciaAlgunArchivoExcelRepExtSegs { get; set; }
		public int IntervaloTiempoCheckeoExistenciaArchivosExcelRepExtSegs { get; set; }
	}
}
