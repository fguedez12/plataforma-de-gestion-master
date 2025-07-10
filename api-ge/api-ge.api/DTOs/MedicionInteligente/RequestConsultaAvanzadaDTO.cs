namespace api_gestiona.DTOs.MedicionInteligente
{
    public class RequestConsultaAvanzadaDTO
    {
        public long Id { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public string Frecuencia { get; set; }
        public List<long> Medidores { get; set; }

    }
}
