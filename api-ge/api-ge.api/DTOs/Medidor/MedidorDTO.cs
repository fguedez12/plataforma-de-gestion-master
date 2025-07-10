using api_gestiona.Entities;

namespace api_gestiona.DTOs.Medidor
{
    public class MedidorDTO
    {
        public long Id { get; set; }
        public string? Numero { get; set; }
        public int Fases { get; set; }
        public bool Smart { get; set; }
        public bool Compartido { get; set; }
        public bool? Factura { get; set; }
        public long NumeroClienteId { get; set; }
        public long? DivisionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long Version { get; set; }
        public bool Active { get; set; }
        public string? ModifiedBy { get; set; }
        public string? CreatedBy { get; set; }
        public bool MedidorConsumo { get; set; }
        public bool Chilemedido { get; set; }
        public int? DeviceId { get; set; }
    }
}
