using api_gestiona.DTOs.Residuos;

namespace api_gestiona.DTOs.Contenedores
{
    public class ContenedoresResponse
    {
        public bool Ok { get; set; }
        public string? Msj { get; set; }
        public List<TipoResiduoDTO> TipoResiduos { get; set; }
        public List<ContenedorDTO> Contenedores { get; set; }
        public ContenedorDTO Contenedor { get; set; }
    }
}
