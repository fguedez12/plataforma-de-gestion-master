using GobEfi.Web.Models.ComunaModels;
using GobEfi.Web.Models.EnergeticoModels;
using System.Collections.Generic;
using System.ComponentModel;

namespace GobEfi.Web.Models.EmpresaDistribuidoraModels
{
    public class EmpresaDistribuidoraModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }

        [DisplayName("Energetico")]
        public long EnergeticoId { get; set; }


        public ICollection<long> Comunas { get; set; }
        public EnergeticoModel Energetico { get; set; }
    }
}
