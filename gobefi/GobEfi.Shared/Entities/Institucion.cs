using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Shared.Entities
{
    public class Institucion : BaseEntity
    {
        public string Nombre { get; set; }
        public int OldId { get; set; }
    }
}
