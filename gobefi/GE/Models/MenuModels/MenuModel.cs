namespace GobEfi.Web.Models.MenuModels
{
    public class MenuModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Url { get; set; }
        public string Icono { get; set; }
        public string IdTag { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Title { get; set; }
    }
}