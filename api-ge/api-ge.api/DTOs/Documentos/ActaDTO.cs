namespace api_gestiona.DTOs.Documentos
{
    public class ActaDTO
    {
        public int Id { get; set; }
        public int TipoDocumentoId { get; set; }
        public string? TipoDocumentoNombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Nresolucion { get; set; }
        public string? AdjuntoUrl { get; set; }
        public string? Adjunto { get; set; }
        public string? AdjuntoPath { get; set; }
        public string? AdjuntoNombre { get; set; }
        public string? Observaciones { get; set; }
        public long ServicioId { get; set; }
        public int EtapaSEV_docs { get; set; }
    }
}
