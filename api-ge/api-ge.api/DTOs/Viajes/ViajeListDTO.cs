using System.Net;

namespace api_gestiona.DTOs.Viajes
{
    public class ViajeListDTO
    {
        public long Id { get; set; }
        public string CiudadOrigen { get; set; }
        public string CiudadDestino { get; set; }
        public string? AeropuertoOrigen { get; set; }
        public string? AeropuertoDestino { get; set; }
        public int NViajesSoloIdaRetorno { get; set; }
        public int NViajesIdaVuelta { get; set; }
        public int Distancia { get; set; }
        public int NumeroViajes 
        {
            get
            {
                
                if(NViajesIdaVuelta>0 && NViajesSoloIdaRetorno == 0) { return 2*NViajesIdaVuelta;}
                if (NViajesIdaVuelta == 0 && NViajesSoloIdaRetorno > 0) { return NViajesSoloIdaRetorno; }
                if (NViajesIdaVuelta > 0 && NViajesSoloIdaRetorno > 0) { return NViajesSoloIdaRetorno+2*NViajesIdaVuelta; }
                return 0;
            }
        }
        public int DistanciaTotal 
        {
            get
            {

                if (NViajesIdaVuelta > 0 && NViajesSoloIdaRetorno == 0) { return 2 * NViajesIdaVuelta * Distancia; }
                if (NViajesIdaVuelta == 0 && NViajesSoloIdaRetorno > 0) { return NViajesSoloIdaRetorno*Distancia; }
                if (NViajesIdaVuelta > 0 && NViajesSoloIdaRetorno > 0) { return (NViajesSoloIdaRetorno + 2 * NViajesIdaVuelta) * Distancia; }
                return 0;
            }
        }
    }
}
