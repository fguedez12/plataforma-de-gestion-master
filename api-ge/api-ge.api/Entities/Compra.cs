namespace api_gestiona.Entities
{
    public class Compra : BaseEntity
    {
        public double Consumo { get; set; }
        public double Costo { get; set; }
        public string? Observacion { get; set; }
        public DateTime InicioLectura { get; set; }
        public DateTime FinLectura { get; set; }
        public DateTime FechaCompra { get; set; }
        public long? NumeroClienteId { get; set; }
        public long? UnidadMedidaId { get; set; }
        public long DivisionId { get; set; }
        public long FacturaId { get; set; }
        public long EnergeticoId { get; set; }
        public string? EstadoValidacionId { get; set; }
        public string? RevisadoPor { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public long CreatedByDivisionId { get; set; }
        public string? ObservacionRevision { get; set; }
        public bool SinMedidor { get; set; }
    }
}
