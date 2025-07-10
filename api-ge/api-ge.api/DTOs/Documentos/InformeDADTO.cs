using System;

namespace api_gestiona.DTOs.Documentos
{
    public class InformeDADTO
    {
        public long ServicioId { get; set; }
        public int TipoDocumentoId { get; set; } = 1016;
        public DateTime Fecha { get; set; }
        public int EtapaSEV_docs { get; set; }
        public string? Adjunto { get; set; }
        public string? AdjuntoPath { get; set; }
    }
}