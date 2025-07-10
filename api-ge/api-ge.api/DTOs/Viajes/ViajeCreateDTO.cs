using api_gestiona.Entities;

namespace api_gestiona.DTOs.Viajes
{
    public class ViajeCreateDTO
    {
        public long Id { get; set; }
        public int? Periodo { get; set; }
        public string? CiudadOrigen { get; set; }
        public string? CiudadDestino { get; set; }
        public int NViajesSoloIdaRetorno { get; set; }
        public int NViajesIdaVuelta { get; set; }
        public long? AeropuertoOrigenId { get; set; }
        public long? AeropuertoDestinoId { get; set; }
        public int Distancia { get; set; }
    }
}
