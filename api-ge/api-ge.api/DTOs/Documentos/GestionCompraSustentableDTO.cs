using System.ComponentModel.DataAnnotations;

namespace api_gestiona.DTOs.Documentos
{
    public class GestionCompraSustentableDTO
    {
        public long Id { get; set; }
        public long TipoDocumentoId { get; set; }
        public string? TipoDocumentoNombre { get; set; }
        public DateTime Fecha { get; set; }
        public string? AdjuntoUrl { get; set; }
        public string? Adjunto { get; set; }
        public string? AdjuntoPath { get; set; }
        public string? AdjuntoNombre { get; set; }
        public string? AdjuntoUrlCompraSustentableAnt { get; set; }
        public string? AdjuntoCompraSustentableAnt { get; set; }
        public string? AdjuntoCompraFuera { get; set; }
        public string? AdjuntoPathCompraSustentableAnt { get; set; }
        public string? AdjuntoNombreCompraSustentableAnt { get; set; }
        public string? AdjuntoUrlCompraFuera { get; set; }
        public string? AdjuntoPathCompraFuera { get; set; }
        public string? AdjuntoNombreCompraFuera { get; set; }
        public int NComprasRubros { get; set; }
        public int? NComprasRubros2 { get; set; }
        public int NComprasRubrosFuera { get; set; }
        public int NComprasCriterios { get; set; }
        public int? NComprasCriterios2 { get; set; }
        public int NComprasCriteriosFuera { get; set; }
        [MaxLength(500)]
        public string? Observaciones { get; set; }
        public long ServicioId { get; set; }
        public int EtapaSEV_docs { get; set; }
    }
}
