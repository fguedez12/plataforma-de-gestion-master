using System;
using System.Collections.Generic;
using System.Text;

namespace GobEfi.FV.Shared.Entities
{
    public class Vehiculo : BaseEntity, IId, IAuditable
    {
        public string Patente { get; set; }
        public string Nombre { get; set; }
        public int Anio { get; set; }
        public long? ModeloId { get; set; }
        public long TipoPropiedadId { get; set; }
        public long MinisterioId { get; set; }
        public long ServicioId { get; set; }
        public int Kilometraje { get; set; }
        public long? RegionId { get; set; }
        public long? ComunaId { get; set; }
        public string Marca { get; set; }
        public string ModeloOtro { get; set; }
        public string Traccion { get; set; }
        public string Transmision { get; set; }
        public long? PropulsionId { get; set; }
        public long? CombustibleId { get; set; }
        public string Cilindrada { get; set; }
        public string Carroceria { get; set; }
        public ModeloEm Modelo { get; set; }
        public Propulsion Propulsion { get; set; }
        public Combustible Combustible { get; set; }
        public List<Imagen> Imagenes { get; set; }
        
    }
}
