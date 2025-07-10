namespace api_gestiona.DTOs
{
    public class BaseResponseDTO<T>
    {
        public bool Ok { get; set; }
        public string? Msj { get; set; }
        public List<T> Dtos { get; set; }
    }
}
