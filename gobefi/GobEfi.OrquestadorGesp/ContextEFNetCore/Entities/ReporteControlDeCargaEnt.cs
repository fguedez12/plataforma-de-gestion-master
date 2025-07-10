using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.ContextEFNetCore
{
	public class ReporteControlDeCargaEnt : _BaseEntity
	{
		public long DivisionId { get; set; }
		public long EnergeticoId { get; set; }
		public string Division { get; set; }
		public long RegionId { get; set; }
		public int RegionPos { get; set; }
		public string RegionNom { get; set; }
		public string Comuna { get; set;}
		public long NroEmpresaDistId{ get; set; }
		public long NroClienteId { get; set; }
		public long MedidorDivisionId { get; set; }
		public string NroCliente { get; set; }
		public string NroMedidorDivision { get; set; }
    public string EmpresaDistribuidora { get; set; }
    public string Energetico { get; set; }
		public bool ReportaPMG { get; set; }
		public long CambioId { get; set; }
		public long MedidorAntiguoId { get; set; }
		public long MedidorNuevoId { get; set; }
    public string NroMedidorAntiguo { get; set; }
    public string NroMedidorNuevo { get; set; }
    //public DateTime FechaIngresoCambioMed { get; set; }
	}
}
