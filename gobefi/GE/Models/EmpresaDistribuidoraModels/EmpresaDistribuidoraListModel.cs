using GobEfi.Web.Models.EnergeticoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EmpresaDistribuidoraModels
{
    public class EmpresaDistribuidoraListModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public long EnergeticoId { get; set; }
        public bool Activo { get; set; }


        public ICollection<long> Comunas { get; set; }
        public EnergeticoModel Energetico { get; set; }
    }
}
