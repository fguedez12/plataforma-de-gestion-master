using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Piso : BaseEntity
    {
        public double Superficie { get; set; }

        public double Altura { get; set; }

        public long NumeroPisoId { get; set; }
        public long? TipoUsoId { get; set; }
        public long? EdificioId { get; set; }
        public long? DivisionId { get; set; }

        public NumeroPiso NumeroPiso { get; set; }

        public virtual Edificio Edificio { get; set; }

        public virtual  Division Inmueble { get; set; }
        public virtual TipoUso TipoUso { get; set; }
        public string NroRol { get; set; }

        public virtual ICollection<Muro> Muros { get; set; }

        public virtual ICollection<Suelo> Suelos { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
        public List<UnidadPiso> UnidadPisos { get; set; }


    }
}
