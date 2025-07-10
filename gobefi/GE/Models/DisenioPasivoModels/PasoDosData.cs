using GobEfi.Web.Models.PisoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.DisenioPasivoModels
{
    public class PasoDosData
    {
        public long? FrontisId { get; set; }
        public bool PisosIguales { get; set; }
        public List<PisoPasoDosModel> Pisos { get; set; }
    }
}
