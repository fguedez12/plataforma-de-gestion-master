using System.Collections.Generic;

namespace GobEfi.Web.Data.Entities
{
    public class Menu
    {
        public long Id { get; set; }
        public long? MenuId { get; set; }
        public Menu ParentMenu { get; set; }
        public long? MenuPanelId { get; set; }
        public string Nombre { get; set; }
        public string Url { get; set; }
        public string Icono { get; set; }
        public string IdTag { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Title { get; set; }
        public int? Orden { get; set; }
        public bool? ActiveV3 { get; set; }
        public string Folder { get; set; }
        public string Component { get; set; }

        public ICollection<Menu> SubMenus { get; set; }
        public MenuPanel MenuPanel { get; set; }
    }
}