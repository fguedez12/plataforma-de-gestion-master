using System;
using System.ComponentModel.DataAnnotations;

namespace OrquestadorGesp.ContextEFNetCore
{
	public class ComprasReporteExtendido : _BaseEntity
	{
		public long IdDivisionCompra { get; set; }
		public long IdCompra { get; set; }
		public long IdCompraMedidor { get; set; }
		public string NomInstitucion { get; set; }
		public string NomServicio { get; set; }
		public long ServicioId { get; set; }
		public string Division { get; set; }
		public string Region { get; set; }
		public int RegionOrder { get; set; }
		public string Comuna { get; set; }
		public float? Superficie { get; set; }
		public long EnergeticoId { get; set; }
		public string Energetico { get; set; }
		public bool EnergeticoPermiteMedidor { get; set; }
		public float? Factor { get; set; }
		public string TipoTransaccion { get; set; }
		public DateTime FechaCompra { get; set; }
		public DateTime InicioLectura { get; set; }
		public DateTime FinLectura { get; set; }
		public float? Cantidad { get; set; }
		public string UnidadMedida { get; set; }
		public long Costo { get; set; }
		public string EmpresaDistribuidora { get; set; }
		public string NumeroDeCliente { get; set; }
		public long IdNumeroDeCliente { get; set; }
		public string NumeroDeMedidor { get; set; }
		public long? IdDeMedidor { get; set; }
		public string EstadoValidacionId { get; set; }
		public string ObservacionCompra { get; set; }
		public string ObservacionRevision { get; set; }
		public string PropiedadMedidor { get; set; }
		public long? IdDivisionDelMedidor { get; set; }
		public long? IdEdificioDelMedidor { get; set; }
	}
}