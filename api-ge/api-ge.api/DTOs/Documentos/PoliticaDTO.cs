namespace api_gestiona.DTOs.Documentos
{
    public class PoliticaDTO
    {
        public long Id { get; set; }
        public long TipoDocumentoId { get; set; }
        public string? TipoDocumentoNombre { get; set; }
        public DateTime Fecha { get; set; }
        public string? AdjuntoUrl { get; set; }
        public string? Adjunto { get; set; }
        public string? AdjuntoPath { get; set; }
        public string? AdjuntoNombre { get; set; }
        public string? AdjuntoRespaldoUrl { get; set; }
        public string? AdjuntoRespaldo { get; set; }
        public string? AdjuntoRespaldoPath { get; set; }
        public string? AdjuntoRespaldoNombre { get; set; }
        public string? AdjuntoRespaldoUrlParticipativo { get; set; }
        public string? AdjuntoRespaldoParticipativo { get; set; }
        public string? AdjuntoRespaldoPathParticipativo { get; set; }
        public string? AdjuntoRespaldoNombreParticipativo { get; set; }
        public int? Cobertura { get; set; }
        public bool Reduccion { get; set; }
        public bool Reutilizacion { get; set; }
        public bool Reciclaje { get; set; }
        public bool ProdBajoImpactoAmbiental { get; set; }
        public bool ProcesoGestionSustentable { get; set; }
        public bool EstandaresSustentabilidad { get; set; }
        public bool GestionPapel { get; set; }
        public bool EficienciaEnergetica { get; set; }
        public bool ComprasSustentables { get; set; }
        public string? Otras { get; set; }
        public string? NResolucionPolitica { get; set; }
        public long ServicioId { get; set; }
        public int EtapaSEV_docs { get; set; }
        public bool? ElaboraPolitica { get; set; }
        public bool? ActualizaPolitica { get; set; }
        public bool? MantienePolitica { get; set; }
        public bool? E1O1RT2 { get; set; }
        public bool? DefinicionesEstrategicas { get; set; }
        public bool? Consultiva { get; set; }
        public bool? ConsultaPersonal { get; set; }
        public string? DefinePolitica { get; set; }
    }
}
