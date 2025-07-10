using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.PermisosModels
{
    public class PermisosAccion
    {
        public long Id { get; set; }
        public bool Lectura { get; set; }
        public bool Escritura { get; set; }
    }
}
