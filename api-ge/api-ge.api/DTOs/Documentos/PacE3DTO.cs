namespace api_gestiona.DTOs.Documentos
{
    public class PacE3DTO
    {
        public long Id { get; set; }
        public long TipoDocumentoId { get; set; }
        public string? TipoDocumentoNombre { get; set; }
        public DateTime Fecha { get; set; }
        public string? AdjuntoUrl { get; set; }
        public string? Adjunto { get; set; }
        public string? AdjuntoPath { get; set; }
        public string? AdjuntoNombre { get; set; }
        public string? AdjuntoRespaldoUrlCompromiso { get; set; }
        public string? AdjuntoRespaldoCompromiso { get; set; }
        public string? AdjuntoRespaldoPathCompromiso { get; set; }
        public string? AdjuntoRespaldoNombreCompromiso { get; set; }
        public long ServicioId { get; set; }
        public int EtapaSEV_docs { get; set; }
    }
}
