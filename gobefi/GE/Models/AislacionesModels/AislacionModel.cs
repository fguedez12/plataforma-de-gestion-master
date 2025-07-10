using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.AislacionesModels
{
    public class AislacionModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public int SubNivel { get; set; }
    }
}
