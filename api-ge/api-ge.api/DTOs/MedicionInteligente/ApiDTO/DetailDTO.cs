namespace api_gestiona.DTOs.MedicionInteligente.ApiDTO
{
    public class DetailDTO
    {
        public int TimeStamp { get; set; }
        public List<SensorValueDTO> SensorValues { get; set; }
    }
}
