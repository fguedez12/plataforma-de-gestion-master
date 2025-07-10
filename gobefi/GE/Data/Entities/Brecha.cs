using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GobEfi.Web.Data.Entities
{
    public class Brecha : BaseEntity
    {
        public long ServicioId { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public long DimensionBrechaId { get; set; }
        public int TipoBrecha { get; set; }
        [Required]
        public string Titulo { get; set; }
        public int Priorizacion { get; set; }
        public long? ObjetivoId { get; set; }
        public DimensionBrecha DimensionBrecha { get; set; }
        public Servicio Servicio { get; set; }
        public ICollection<BrechaUnidad> BrechaUnidades { get; set; }
        public Objetivo Objetivo { get; set; }

    }
}
