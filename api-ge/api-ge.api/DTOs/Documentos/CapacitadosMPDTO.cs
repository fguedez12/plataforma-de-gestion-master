namespace api_gestiona.DTOs.Documentos
{
    public class CapacitadosMPDTO
    {
        public long Id { get; set; }
        public long TipoDocumentoId { get; set; }
        public string? TipoDocumentoNombre { get; set; }
        public DateTime Fecha { get; set; }
        public string? AdjuntoUrl { get; set; }
        public string? Adjunto { get; set; }
        public string? AdjuntoPath { get; set; }
        public string? AdjuntoNombre { get; set; }
        public int? TotalColaboradoresCapacitados { get; set; }
        public string? FuncionariosDesignados { get; set; }
        public int FuncionariosDesignadosNum { get; set; }
        public long ServicioId { get; set; }
        public string? Observaciones { get; set; }
        public int EtapaSEV_docs { get; set; }
        public float? PorcentajeCapacitadosEtapa2 { get; set; }
    }
}
