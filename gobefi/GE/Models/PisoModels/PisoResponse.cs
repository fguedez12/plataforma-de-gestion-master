using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.PisoModels
{
    public class PisoResponse
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }
        public List<PisoModel> PisoList { get; set; }
        public List<PisoPasoUnoModel> PisoPasoUnoList { get; set; }
    }
}
