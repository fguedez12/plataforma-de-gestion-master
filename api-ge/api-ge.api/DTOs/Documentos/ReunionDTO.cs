using System.ComponentModel.DataAnnotations;

namespace api_gestiona.DTOs.Documentos
{
    public class ReunionDTO
    {
        public long Id { get; set; }
        public long TipoDocumentoId { get; set; }
        public long ActaComiteId { get; set; }
        public string? TipoDocumentoNombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Titulo { get; set; }
        public string? AdjuntoUrl { get; set; }
        public string? Adjunto { get; set; }
        public string? AdjuntoPath { get; set; }
        public string? AdjuntoNombre { get; set; }
        [MaxLength(500)]
        public string? Observaciones { get; set; }
        public bool? ApruebaAlcanceGradualSEV { get; set; }
        public bool RevisionPoliticaAmbiental { get; set; }
        public bool? DetActDeConcientizacion { get; set; }
        public bool? RevisionProcBienesMuebles { get; set; }
        public bool? ApruebaDiagnostico { get; set; }
        public bool? PuestaEnMarchaCEV { get; set; }
        public bool? DefinePropuestaConcientizados { get; set; }
        public long ServicioId { get; set; }
        public int EtapaSEV_docs { get; set; }
        public string? DefinePolitica { get; set; }
    }
}
