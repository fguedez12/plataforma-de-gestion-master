namespace api_gestiona.DTOs.Documentos
{
    public class DocumentoResponse
    {
        public bool Ok { get; set; }
        public string? Msj { get; set; }
        public bool? NoRegistraPoliticaAmbiental { get; set; }
        public bool? NoRegistraDifusionInterna { get; set; }
        public bool? NoRegistraActividadInterna { get; set; }
        public bool? NoRegistraReutilizacionPapel { get; set; }
        public bool? NoRegistraProcFormalPapel { get; set; }
        public bool? NoRegistraDocResiduosCertificados { get; set; }
        public bool? NoRegistraDocResiduosSistemas { get; set; }
        public bool? NoRegistraProcBajaBienesMuebles { get; set; }
        public bool? NoRegistraProcComprasSustentables { get; set; }
        public List<ActaDTO> Actas { get; set; }
        public ActaDTO Acta { get; set; }
        public ReunionDTO Reunion { get; set; }
        public ListaIntegrantesDTO ListaIntegrante { get; set; }
        public List<ComiteDTO> Comites { get; set; }
        public List<PoliticaListaDTO> PoliticaLista { get; set; }
        public List<PgaListaDTO> PgaLista { get; set; }
        public PoliticaDTO Politica { get; set; }
        public DifusionDTO Difusion { get; set; }
        public ProcedimientoPapelDTO ProcedimientoPapel { get; set; }
        public ProcedimientoResiduoDTO ProcedimientoResiduo { get; set; }
        public ProcedimientoResiduoSistemaDTO ProcedimientoResiduoSistema { get; set; }
        public ProcedimientoBajaBienesDTO ProcedimientoBajaBienes { get; set; }
        public ProcedimientoCompraSustentableDTO ProcedimientoCompraSustentable { get; set; }
        public ProcReutilizacionPapelDTO ProcedimientoReutilizacionPapel { get; set; }
        public CharlasDTO Charla { get; set; }
        public CapacitadosMPDTO CapacitadoMP { get; set; }
        public PacE3DTO PacE3 { get; set; }
        public ResolucionApruebaPlanDTO? ResolucionApruebaPlanDTO { get; set; }
        public ListadoColaboradorDTO ListadoColaborador { get; set; }
        public GestionCompraSustentableDTO CompraSustentable { get; set; }
        public List<ProcedimientoListaDto> Procedimientos { get; set; }
        public List<CharlaListaDTO> Charlas { get; set; }
        public List<CapacitadosMPDTO> CapacitadosMP { get; set; }
        public List<PacE3DTO> PacE3s { get; set; }
        public List<GestionCompraSustentableDTO> CompraSustentables { get; set; }
        public InformeDADTO InformeDA { get; set; }

    }
}
