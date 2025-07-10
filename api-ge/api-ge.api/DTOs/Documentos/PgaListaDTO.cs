namespace api_gestiona.DTOs.Documentos
{
    public class PgaListaDTO
    {
        public long Id { get; set; }
        public int TipoDocumentoId { get; set; }
        public string? TipoDocumentoNombre { get; set; }
        public DateTime Fecha { get; set; }
        public string NResolucionPolitica { get; set; }
    }
}
