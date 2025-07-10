using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GobEfi.Web.Data.Entities
{
    public class DimensionBrecha
    {
        public long Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string NombreNormalizado { get; set; }
        [Required]
        public string Target { get; set; }
        public ICollection<DimensionServicio> DimensionServicios { get; set; }
        public ICollection<Brecha> Brechas { get; set; }
        public ICollection<Objetivo> Objetivos { get; set; }
        public ICollection<Medida> Medidas { get; set; }
        public ICollection<Accion> Acciones { get; set; }
        public ICollection<Tarea> Tareas { get; set; }
        public ICollection<Indicador> Indicadores { get; set; }
    }
}
