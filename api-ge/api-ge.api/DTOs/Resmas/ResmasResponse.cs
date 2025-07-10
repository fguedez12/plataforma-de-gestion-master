namespace api_gestiona.DTOs.Resmas
{
    public class ResmasResponse
    {
        public bool Ok { get; set; }
        public string Msj { get; set; }
        public List<ResmaDTO> Resmas { get; set; }
        public ResmaDTO Resma { get; set; }
    }
}
