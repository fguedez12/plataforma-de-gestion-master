namespace api_gestiona.DTOs.Documentos
{
    public class ComiteDTO
    {
        public long Id { get; set; }
        public string Nresolucion { get; set; }
        public int TipoDocumentoId { get; set; }
        public string? TipoDocumentoNombre { get; set; }
        public string? TipoDocumentoNombreE2 { get; set; }
        public List<string>? ListaTemas { get; set; }
        public DateTime Fecha { get; set; }
    }
}
