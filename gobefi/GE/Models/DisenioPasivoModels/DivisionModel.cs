using GobEfi.Web.Models.EdificioModels;
using GobEfi.Web.Models.PisoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.DisenioPasivoModels
{
    public class DivisionModel
    {
        public bool PisosIguales { get; set; }
        public EdificioDPModel Edificio  { get; set; }
        public List<PisoModel> Pisos { get; set; }
        public int NivelPaso3 { get; set; }
        public List<ArchivoDpModel> Archivos { get; set; }
    }
}
