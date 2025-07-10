using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GobEfi.Web.Data.Entities
{
    public class Medida
    {
        public long Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public long? DimensionbrechaId { get; set; }
        public int TipoMedida { get; set; }
        public string Agrupacion { get; set; }
        public string TipoDeMedida { get; set; }
        public DimensionBrecha DimensionBrecha { get; set; }
        public ICollection<Medida> Medidas { get; set; }
    }
}
