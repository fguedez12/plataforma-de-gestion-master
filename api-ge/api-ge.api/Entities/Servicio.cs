namespace api_gestiona.Entities
{
    public class Servicio : BaseEntity
    {
        public string? Nombre { get; set; }
        public long InstitucionId { get; set; }
        public string? Justificacion { get; set; }
        public bool ReportaPMG { get; set; }
        public bool RevisionRed { get; set; } = false;
        public bool? NoDeclaraViajeAvion { get; set; }
        public bool? NoRegistraPoliticaAmbiental { get; set; }
        public bool? NoRegistraDifusionInterna { get; set; }
        public bool? NoRegistraActividadInterna { get; set; }
        public bool? NoRegistraReutilizacionPapel { get; set; }
        public bool? NoRegistraProcFormalPapel { get; set; }
        public bool? NoRegistraDocResiduosCertificados { get; set; }
        public bool? NoRegistraDocResiduosSistemas { get; set; }
        public bool? NoRegistraProcBajaBienesMuebles { get; set; }
        public bool? NoRegistraProcComprasSustentables { get; set; }
        public string? ComentarioRed { get; set; }
        public bool RevisionDiagnosticoAmbiental { get; set; }
        public bool ModificacioAlcance { get; set; }
        public int ColaboradoresModAlcance { get; set; }
        public bool CompraActiva { get; set; }
        public int EtapaSEV { get; set; }
        public bool BloqueoIngresoInfo { get; set; }
        public bool? NoDeclaraViajeAvion2025 { get; set; }
        public bool? PgaRevisionRed { get; set; }
        public string? PgaObservacionRed{ get; set; }
        public string? PgaRespuestaRed{ get; set; }
        public bool? ValidacionConcientizacion { get; set; }
        public Institucion Institucion { get; set; }
        public List<UsuarioServicio> UsuariosServicios { get; set; }
        public List<Division> Divisiones { get; set; }
        public List<Documento> Documentos { get; set; }
        public List<Viaje> Viajes { get; set; }
        public List<Brecha>? Brechas { get; set; }
        public ICollection<DimensionServicio>? DimensionServicios { get; set; }
        public ICollection<AccionServicio>? AccionServicios { get; set; }
        public ICollection<Programa>? Programas { get; set; }
    }
}
