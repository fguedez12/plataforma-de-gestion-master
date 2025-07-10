using System.Collections.Generic;
using GobEfi.Web.Models.MenuModels;

namespace GobEfi.Web.Models.MenuPanelModels
{
    public class MenuPanelModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string TagId { get; set; }
        public string TagHref { get; set; }
        
        public ICollection<MenuModel> Menus { get; set; }
    }
}