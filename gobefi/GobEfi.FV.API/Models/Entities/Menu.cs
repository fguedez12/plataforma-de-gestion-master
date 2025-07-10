using GobEfi.FV.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Models.Entities
{
    public class Menu : IId
    {
        public long Id { get ; set ; }
        public string Nombre { get; set; }
        public string Url { get; set; }
        public string Icono { get; set; }
        public int Orden { get; set; }
        public List<Permiso> Permisos { get; set; }
    }
}
