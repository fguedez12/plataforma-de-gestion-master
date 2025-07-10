namespace api_gestiona.Entities
{
    public class Medidor :BaseEntity
    {
        public string? Numero { get; set; }
        public int Fases { get; set; }
        public bool Smart { get; set; }
        public bool Compartido { get; set; }
        public bool? Factura { get; set; }
        public long NumeroClienteId { get; set; }
        public long? DivisionId { get; set; }
        public bool MedidorConsumo { get; set; } = true;
        public bool Chilemedido { get; set; } = false;
        public int? DeviceId { get; set; }
        public Division? Division { get; set; }
    }
}
