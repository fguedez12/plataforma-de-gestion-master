namespace api_gestiona.DTOs.Residuos
{
    public class ResiduosResponse
    {
        public bool Ok { get; set; }
        public string? Msj { get; set; }
        public bool? NoDeclaraContenedores { get; set; }
        public List<ResiduoDTO> Residuos { get; set; }
        public ResiduoDTO Residuo { get; set; }
    }
}
