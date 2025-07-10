namespace api_gestiona.DTOs.Viajes
{
    public class ViajeResponseDTO
    {
        public List<ViajeListDTO> Viajes { get; set; }
        public bool? NoDeclaraViajeAvion { get; set; }
    }
}
