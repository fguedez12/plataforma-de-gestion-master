using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EnergeticoModels
{
    public class EnergeticoForEditModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Permite medidor")]
        public bool PermiteMedidor { get; set; }
        public bool Activo { get; set; }
        [DisplayName("Unidad de medida")]
        public ICollection<long> UnidadesDeMedidasId { get; set; }
    }
}
