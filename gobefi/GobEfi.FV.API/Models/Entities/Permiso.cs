using GobEfi.FV.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Models.Entities
{
    public class Permiso : IId
    {
        public long Id { get ; set ; }
        public long MenuId { get; set; }
        public string Role { get; set; }
        public bool Lectura { get; set; }
        public bool Escritura { get; set; }
        public Menu Menu { get; set; }
    }
}
