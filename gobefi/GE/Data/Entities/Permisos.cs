
namespace GobEfi.Web.Data.Entities
{
    public class Permisos
    {
        public long Id { get; set; }
        public long MenuId { get; set; }
        //public long? SubMenuId { get; set; }
        public string RolId { get; set; }
        public bool Lectura { get; set; }
        public bool Escritura { get; set; }

        public Menu Menu { get; set; }
        //public SubMenu SubMenu { get; set; }
        public Rol Rol { get; set; }
    }
}