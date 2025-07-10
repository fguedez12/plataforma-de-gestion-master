using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Models.DTOs
{
    public class PermisoDTO
    {
        public long Id { get; set; }
        public long MenuId { get; set; }
        public string Role { get; set; }
        public bool Lectura { get; set; }
        public bool Escritura { get; set; }
        public MenuDTO Menu { get; set; }
    }
}
