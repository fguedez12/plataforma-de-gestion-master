namespace api_gestiona.DTOs.Documentos
{
    public class ProcedimientoListaDto
    {
        public long Id { get; set; }
        public int TipoDocumentoId { get; set; }
        public string? TipoDocumentoNombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Titulo { get; set; }
        public string? NResolucionProcedimiento { get; set; }
    }

}
