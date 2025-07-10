namespace api_gestiona.DTOs.Documentos
{
    public class ProcedimientoPapelDTO
    {
        public long Id { get; set; }
        public long TipoDocumentoId { get; set; }
        public string? TipoDocumentoNombre { get; set; }
        public DateTime Fecha { get; set; }
        public string? AdjuntoUrl { get; set; }
        public string? Adjunto { get; set; }
        public string? AdjuntoPath { get; set; }
        public string? AdjuntoNombre { get; set; }
        public int? Cobertura { get; set; }
        public bool DifusionInterna { get; set; }
        public bool Implementacion { get; set; }
        public bool Reduccion { get; set; }
        public bool Reutilizacion { get; set; }
        public bool ImpresionDobleCara { get; set; }
        public bool BajoConsumoTinta { get; set; }
        public string Titulo { get; set; }
        public long? PoliticaId { get; set; }
        public string? NResolucionProcedimiento { get; set; }
        public bool FormatoDocumento { get; set; }
        public long ServicioId { get; set; }
        public int EtapaSEV_docs { get; set; }
    }
}
