using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Models.DTOs
{
    public class MenuDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Url { get; set; }
        public string Icono { get; set; }
        public int Orden { get; set; }
        public List<PermisoDTO> Permisos { get; set; }
    }
}
