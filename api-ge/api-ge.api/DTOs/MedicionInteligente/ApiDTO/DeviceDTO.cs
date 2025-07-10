namespace api_gestiona.DTOs.MedicionInteligente.ApiDTO
{
    public class DeviceDTO
    {
        public int Id { get; set; }
        public Dictionary<string,DetailDTO> Measures { get; set; }
    }
}
