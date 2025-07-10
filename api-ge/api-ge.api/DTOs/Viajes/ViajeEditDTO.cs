namespace api_gestiona.DTOs.Viajes
{
    public class ViajeEditDTO
    {
        public long Id { get; set; }
        public int? Periodo { get; set; }
        public string CiudadOrigen { get; set; }
        public string CiudadDestino { get; set; }
        public long? AeropuertoOrigenId { get; set; }
        public long? AeropuertoDestinoId { get; set; }
        public long? PaisOrigenId { get; set; }
        public long? PaisDestinoId { get; set; }
        public int NViajesSoloIdaRetorno { get; set; }
        public int NViajesIdaVuelta { get; set; }
        public int Distancia { get; set; }
        public int NViajesTotales
        {
            get
            {

                if (NViajesIdaVuelta > 0 && NViajesSoloIdaRetorno == 0) { return 2 * NViajesIdaVuelta; }
                if (NViajesIdaVuelta == 0 && NViajesSoloIdaRetorno > 0) { return NViajesSoloIdaRetorno; }
                if (NViajesIdaVuelta > 0 && NViajesSoloIdaRetorno > 0) { return NViajesSoloIdaRetorno + 2 * NViajesIdaVuelta; }
                return 0;
            }
        }
        public int DistanciaEstimada
        {
            get
            {

                if (NViajesIdaVuelta > 0 && NViajesSoloIdaRetorno == 0) { return 2 * NViajesIdaVuelta * Distancia; }
                if (NViajesIdaVuelta == 0 && NViajesSoloIdaRetorno > 0) { return NViajesSoloIdaRetorno * Distancia; }
                if (NViajesIdaVuelta > 0 && NViajesSoloIdaRetorno > 0) { return (NViajesSoloIdaRetorno + 2 * NViajesIdaVuelta) * Distancia; }
                return 0;
            }
        }
    }
}
