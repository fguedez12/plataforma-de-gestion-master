using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Ajuste : BaseEntity
    {
        public bool EditUnidadPMG { get; set; }
        public bool DeleteUnidadPMG { get; set; }
        public bool ComprasServicio { get; set; }
        public bool CreateUnidadPMG { get; set; }
        public bool ActiveAlcanceModule { get; set; }
    }
}
