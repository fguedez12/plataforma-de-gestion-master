namespace api_gestiona.Entities
{
    public class NumeroCliente : BaseEntity
    {
        public string? Numero { get; set; }
        public string? NombreCliente { get; set; }
        public decimal PotenciaSuministrada { get; set; }
        public long? EmpresaDistribuidoraId { get; set; }
        public long? TipoTarifaId { get; set; }
    }
}
