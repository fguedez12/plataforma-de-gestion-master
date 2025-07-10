namespace GobEfi.Web.Data.Entities
{
    public class PermisosBackEnd
    {
        public long Id { get; set; }
        public long EndPointId { get; set; }
        public string RolId { get; set; }
        public EndPoint EndPoint { get; set; }
        public Rol Rol { get; set; }
    }
}
