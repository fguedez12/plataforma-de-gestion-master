using System.Collections.Generic;

namespace GobEfi.Web.Data.Entities
{
    public class MenuPanel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string TagId { get; set; }
        public string TagHref { get; set; }
        
        public ICollection<Menu> Menus { get; set; }
    }
}