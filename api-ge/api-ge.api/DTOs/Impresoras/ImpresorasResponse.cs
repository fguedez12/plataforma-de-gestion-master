namespace api_gestiona.DTOs.Impresoras
{
    public class ImpresorasResponse
    {
        public bool Ok { get; set; }
        public string Msj { get; set; }
        public bool? NoDeclaraImpresora { get; set; }
        public ImpresoraDTO Impresora { get; set; }
        public List<ImpresoraDTO> Impresoras { get; set; }
    }
}
