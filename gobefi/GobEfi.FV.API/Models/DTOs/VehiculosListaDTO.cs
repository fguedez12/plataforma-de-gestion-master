namespace GobEfi.FV.API.Models.DTOs
{
    public class VehiculosListaDTO
    {
        public long Id { get; set; }
        public string Patente { get; set; }
        public int Anio { get; set; }
        public int Kilometraje { get; set; }
        public long ServicioId { get; set; }
        public int TipoPropiedadId { get; set; }
        public long ModeloId { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public long MinisterioId { get; set; }
        public long ComunaId { get; set; }
        public long RegionId { get; set; }
        public string Carroceria { get; set; }
        public string Cilindrada { get; set; }
        public long CombustibleId { get; set; }
        public string Combustible { get; set; }
        public string MarcaOtro { get; set; }
        public string ModeloOtro { get; set; }
        public long PropulsionId { get; set; }
        public string Propulsion { get; set; }
        public string Traccion { get; set; }
        public string Transmision { get; set; }
    }
}
