namespace api_gestiona.DTOs.EncuestaColaborador
{
    public class EncuestaColaboradorResponseDTO
    {
        public int TotalEncuestados { get; set; }
        public VehiculoPropioDTO VehiculoPropio { get; set; }
        public VehiculoCompartidoDTO VehiculoCompartido { get; set; }
        public TransportePublicoDTO TransportePublico { get; set; }
        public MotocicletaDTO Motocicleta { get; set; }
        public BicicletaDTO Bicicleta { get; set; }
        public OtrasFormasDTO OtrasFormas { get; set; }
        public long ServicioId { get; set; }
    }
}
