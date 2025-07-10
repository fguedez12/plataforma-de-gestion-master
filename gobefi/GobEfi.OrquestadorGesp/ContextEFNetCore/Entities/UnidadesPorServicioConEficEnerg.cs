using System.ComponentModel.DataAnnotations;

namespace OrquestadorGesp.ContextEFNetCore
{
	public class UnidadesPorServicioConEficEnerg: _BaseEntity
	{
		[Key]
		public long IdUnidad { get; set; }
		public string NombreServicio { get; set; }
		public long ServicioId { get; set; }
		public long AnexoId { get; set; }
		public long RegionId { get; set; }
		public int RegionPos { get; set; }
		public string NombreComuna { get; set; }
		public string NombreUnidad { get; set; }
	}
}
