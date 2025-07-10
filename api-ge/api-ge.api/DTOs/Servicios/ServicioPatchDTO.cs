namespace api_gestiona.DTOs.Servicios
{
    public class ServicioPatchDTO
    {
        public bool? NoRegistraPoliticaAmbiental { get; set; }
        public bool? NoRegistraDifusionInterna { get; set; }
        public bool? NoRegistraActividadInterna { get; set; }
        public bool? NoRegistraReutilizacionPapel { get; set; }
        public bool? NoRegistraProcFormalPapel { get; set; }
        public bool? NoRegistraDocResiduosCertificados { get; set; }
        public bool? NoRegistraDocResiduosSistemas { get; set; }
        public bool? NoRegistraProcBajaBienesMuebles { get; set; }
        public bool? NoRegistraProcComprasSustentables { get; set; }
        public bool? RevisionDiagnosticoAmbiental { get; set; }
        public int? ColaboradoresModAlcance { get; set; }
        public bool? CompraActiva { get; set; }
        public bool ModificacioAlcance { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public long Version { get; set; }

    }
}
