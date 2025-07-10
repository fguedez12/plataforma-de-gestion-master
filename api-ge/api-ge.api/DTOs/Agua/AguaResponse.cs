namespace api_gestiona.DTOs.Agua
{
    public class AguaResponse
    {
        public bool Ok { get; set; }
        public string Msj { get; set; }
        public bool? NoDeclaraArtefactos { get; set; }
        public bool UsaBidon { get; set; }
        public int? AccesoFacturaAgua { get; set; }
        public bool? ComparteMedidorAgua { get; set; }
        public List<AguaDTO> Consumos { get; set; }
        public AguaDTO Consumo { get; set; }
    }
}
