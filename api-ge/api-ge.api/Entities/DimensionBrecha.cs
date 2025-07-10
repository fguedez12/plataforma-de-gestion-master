namespace api_gestiona.Entities
{
    public class DimensionBrecha
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string NombreNormalizado { get; set; } = null!;
        public string Target { get; set; } = null!;
        public ICollection<DimensionServicio>? DimensionServicios { get; set; }
        public ICollection<Brecha>? Brechas { get; set; }
        public ICollection<Objetivo>? Objetivos { get; set; }
        public ICollection<Indicador>? Indicadores { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
