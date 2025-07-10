using GobEfi.Web.Models.PisoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.MuroModels
{
    public class MurosForSave
    {
        public int? Level { get; set; }
        public bool PisosIguales { get; set; }
        public PisoModel[] pisos { get; set; }
    }
}
