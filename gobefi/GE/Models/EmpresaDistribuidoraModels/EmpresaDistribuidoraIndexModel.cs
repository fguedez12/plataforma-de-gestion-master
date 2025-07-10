using GobEfi.Web.Models.EnergeticoModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EmpresaDistribuidoraModels
{
    public class EmpresaDistribuidoraIndexModel : BasePagedModel<EmpresaDistribuidoraListModel>
    {
        public string Nombre { get; set; }

        [DisplayName("Energeticos")]
        public long EnergeticoId { get; set; }

        [DisplayName("Regiones")]
        public long RegionId { get; set; }

        [DisplayName("Provincias")]
        public long ProvinciaId { get; set; }

        [DisplayName("Comunas")]
        public long ComunaId { get; set; }
        public bool Activo { get; set; }


        public ICollection<long> Comunas { get; set; }
        public EnergeticoModel Energetico { get; set; }
    }
}
