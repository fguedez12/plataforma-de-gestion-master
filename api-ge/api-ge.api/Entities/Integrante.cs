namespace api_gestiona.Entities
{
    public class Integrante
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string AreaInst { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
        public bool Marca { get; set; }
        public long ListaIntegranteId { get; set; }
        public ListaIntegrante ListaIntegrante { get; set; }
    }
}
