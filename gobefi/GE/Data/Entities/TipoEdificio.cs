using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class TipoEdificio
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public long OldId { get; set; }

        public IEnumerable<Edificio> Edificios { get; set; }

    }
}