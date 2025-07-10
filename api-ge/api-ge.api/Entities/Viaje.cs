namespace api_gestiona.Entities
{
    public class Viaje : BaseEntity
    {
        public int? Periodo { get; set; }
        public string CiudadOrigen { get; set; }
        public string CiudadDestino { get; set; }
        public int NViajesSoloIdaRetorno { get; set; }
        public int NViajesIdaVuelta { get; set; }
        public int Distancia { get; set; }
        public long ServicioId { get; set; }
        public long? AeropuertoOrigenId { get; set; }
        public long? AeropuertoDestinoId { get; set; }
        public int? Year { get; set; }
        public Aeropuerto? AeropuertoOrigen { get; set; }
        public Aeropuerto? AeropuertoDestino { get; set; }
        public Servicio Servicio { get; set; }
    }
}
