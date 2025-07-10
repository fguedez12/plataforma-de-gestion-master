using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.DTOs
{
	public class RegistroInsumoRepExtControlCarga : BaseDTO
	{
		public int IdDivisionCompra { get; set;}
		public string TipoTransaccion { get; set; }
		public DateTime InicioLectura { get; set; }
		public DateTime FinLectura { get; set; }
		public long IdNumeroDeCliente { get; set; }
		public long IdMedidor { get; set; }
		public string EstadoValidacion { get; set; }
		public byte EnergeticoId { get; set; }
	}
}
