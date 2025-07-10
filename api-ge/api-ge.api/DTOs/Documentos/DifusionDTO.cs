namespace api_gestiona.DTOs.Documentos
{
    public class DifusionDTO
    {
        public long Id { get; set; }
        public long TipoDocumentoId { get; set; }
        public DateTime Fecha { get; set; }
        public string? AdjuntoUrl { get; set; }
        public string? Adjunto { get; set; }
        public string? AdjuntoPath { get; set; }
        public string? AdjuntoNombre { get; set; }
        public string Titulo { get; set; }
        public long PoliticaId { get; set; }
        public long ServicioId { get; set; }
    }
}
