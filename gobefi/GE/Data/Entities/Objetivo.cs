using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GobEfi.Web.Data.Entities
{
    public class Objetivo : BaseEntity
    {
        public long DimensionBrechaId { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        [MaxLength(100)]
        public string Descripcion { get; set; }
        public DimensionBrecha DimensionBrecha { get; set; }
        public ICollection<Brecha> Brechas { get; set; }
        public ICollection<Accion> Acciones { get; set; }
        public ICollection<Objetivo> Objetivos { get; set; }
    }
}
