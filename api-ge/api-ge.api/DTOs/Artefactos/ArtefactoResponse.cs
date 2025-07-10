namespace api_gestiona.DTOs.Artefactos
{
    public class ArtefactoResponse
    {
        public bool Ok { get; set; }
        public string? Msj { get; set; }
        public List<ArtefactoDTO> Artefactos { get; set; }
        public ArtefactoDTO Artefacto { get; set; }
        public List<TipoArtefactosDTO> TipoArtefactos { get; set; }
    }
}
