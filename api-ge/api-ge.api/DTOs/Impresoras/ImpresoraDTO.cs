using System.ComponentModel.DataAnnotations;

namespace api_gestiona.DTOs.Impresoras
{
    public class ImpresoraDTO
    {
        public long Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public float? Potencia { get; set; }
        public int NumeroImpresiones { get; set; }
        public int BcnColor { get; set; }
        public int Caras { get; set; }
        [Required]
        public string TipoImpresora { get; set; }
        public long DivisionId { get; set; }

    }
}
