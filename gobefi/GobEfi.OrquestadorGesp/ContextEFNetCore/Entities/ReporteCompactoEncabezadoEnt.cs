using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.ContextEFNetCore
{
	public class ReporteCompactoEncabezadoEnt : _BaseEntity
	{
		public long DivisionId { get; set; }
		public string Division { get; set; }
		public long RegionId { get; set; }
		public int RegionPos { get; set; }
		public string RegionNom { get; set; }
		public string Comuna { get; set;}
		public float? Superficie { get; set; }
		public long EnergeticoId { get; set; }
		public string Energetico { get; set; }
		public bool MedidorMixto { get; set; }
		public bool MedidorExclusivo { get; set; }
		public string ClasificacionEdificio { get; set; }
	}
}
