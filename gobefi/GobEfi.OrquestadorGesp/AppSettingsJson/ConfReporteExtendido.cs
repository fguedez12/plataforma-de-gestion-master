namespace OrquestadorGesp.AppSettingsJson
{
	public class ConfReporteExtendido : ConfReporteBase
	{
		public new const string VALOR_CAMPO_SIN_INFO = "";
		public const string VALOR_CAMPO_CALCULADO_SIN_INFO = "-";
		public const string PLANTILLA_COD_SQL_EJECUTAR_SP_CON_UN_PARAM = "EXECUTE {0} {1}";
		public string NombreUnidadConversionDestino { get; set; }
		public string NombreCampoFactor { get; set; }
		public string NombreCampoTipoTransaccion { get; set; }
		public string ValorTipoTransaccionCompra { get; set; }
		public string ValorTipoTransaccionConsumo { get; set; }
		public string[] ColumnasNombresCamposExcelConDefaultEspecial { get; set; }
		public string ValorDefectoEspecial { get; set; }
		public string NombreCampoFinLectura { get; set; }

		public string NombreCampoNombreServicio { get; set; }
		public string NombreCampoIdServicio { get; set; }
		public string NombreCampoUnidadMedida { get; set; }
		public long IdCompraMedidorSinMedidor { get; set; }

    public string[] SubRutasParaInsumo { get; set; }

  }
}