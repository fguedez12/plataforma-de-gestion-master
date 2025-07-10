namespace OrquestadorGesp.ContextEFNetCore
{
	public class UnidadesPorCadaServicioEnt : _BasePkEntity
	{
		public string NombreServicio { get; set; }
		public long ServicioId { get; set; }
		public long RegionId { get; set; }
		public string NombreComuna { get; set; }
		public string DireccionEdificio { get; set; }
		public long IdEdificio { get; set; }
		public string NombreUnidad { get; set; }
		public long IdUnidad { get; set; }
		public double SuperficieUnidad { get; set; }
		public bool ComparteMedidorElectricidad { get; set; }
		public bool ComparteMedidorGasCanheria { get; set; }
		public bool MedicionExclusiva { get; set; }
		public int NroMedidoresElectricidad { get; set; }
		public int NroMedidoresGN { get; set; }
		public string TipoEdificio { get; set; }
		public string NroDeRol { get; set; }
		public int NroDeFuncionarios { get; set; }
		public int AnhoConstruccion { get; set; }
		public string TipoPropiedad { get; set; }
	}
}
