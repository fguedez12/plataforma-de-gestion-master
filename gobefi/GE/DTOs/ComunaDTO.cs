using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.DTOs
{
    public class ComunaDTO : IDTOBase
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
    }
}
