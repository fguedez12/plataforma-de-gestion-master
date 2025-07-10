namespace api_gestiona.DTOs.Documentos
{
    public class ProcedimientoResiduoSistemaDTO
    {
        public long Id { get; set; }
        public long TipoDocumentoId { get; set; }
        public string? TipoDocumentoNombre { get; set; }
        public DateTime Fecha { get; set; }
        public string? AdjuntoUrl { get; set; }
        public string? Adjunto { get; set; }
        public string? AdjuntoPath { get; set; }
        public string? AdjuntoNombre { get; set; }
        public bool Reduccion { get; set; }
        public bool Reutilizacion { get; set; }
        public bool Reciclaje { get; set; }
        public string Titulo { get; set; }
        public long? PoliticaId { get; set; }
        public string? NResolucionProcedimiento { get; set; }
        public long ServicioId { get; set; }
        public int EtapaSEV_docs { get; set; }
    }
}
