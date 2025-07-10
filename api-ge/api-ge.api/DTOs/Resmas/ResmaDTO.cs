using api_gestiona.Entities;
using api_gestiona.Validations;

namespace api_gestiona.DTOs.Resmas
{
    public class ResmaDTO
    {
        public long Id { get; set; }
        public bool Agregada { get; set; }
        public int? CantidadResmasRecicladas { get; set; }
        public int? PapelRecicladoRango { get; set; }
        public int CantidadResmas { get; set; }
        [ResmaNoAgregadaRequiredValidations]
        public DateTime? FechaAdquisicion { get; set; }
        [ResmaAgregadaRequiredValidations]
        public int? AnioAdquisicion { get; set; }
        public int CostoEstimado { get; set; }
        public string? IdMercadoPublico { get; set; }
        public int DivisionId { get; set; }
    }
}
