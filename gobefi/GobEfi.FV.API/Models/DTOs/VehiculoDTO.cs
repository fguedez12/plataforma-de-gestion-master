using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Models.DTOs
{
    public class VehiculoDTO
    {
        public long Id { get; set; }
        public string Patente { get; set; }
        public string Nombre { get; set; }
        public int Anio { get; set; }
        public long? ModeloId { get; set; }
        public long TipoPropiedadId { get; set; }
        public string TipoVehiculo { get; set; }
        public string Marca { get; set; }
        public string Modelootro { get; set; }
        public string Traccion { get; set; }
        public string Transmision { get; set; }
        public long? TransmisionId { get; set; }
        public long? CombustibleId { get; set; }
        public long? PropulsionId { get; set; }
        public string Cilindrada { get; set; }
        public string Carroceria { get; set; }
        public string Propulsion { get; set; }
        public long MinisterioId { get; set; }
        public long ServicioId { get; set; }
        public long? RegionId { get; set; }
        public long? ComunaId { get; set; }
        public int Kilometraje { get; set; }
        public string Modelo { get; set; }
        public string KilometrosPorLitro { get; set; }
        public List<ImagenDTO> Imagenes { get; set; }

    }
}
