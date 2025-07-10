using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Helpers
{
    public class ImpersonalizacionSettings
    {
        public bool Impersonalizar { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string Dominio { get; set; }
    }
}
