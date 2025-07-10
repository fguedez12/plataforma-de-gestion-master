namespace GobEfi.Web.Models.PermisosModels
{
    public class PermisosModel
    {
        public long Id { get; set; }
        public long MenuId { get; set; }
        public long SubMenuId { get; set; }
        public string RolId { get; set; }
        public bool Lectura { get; set; }
        public bool Escritura { get; set; }

        public bool PermisoPorAccion { get; set; }

        public string EscrituraHabilitado
        {
            get
            {
                return Escritura ? "" : "disabled";
            }
        }
    }
}