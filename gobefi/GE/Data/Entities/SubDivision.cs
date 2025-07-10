using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class SubDivision : BaseEntity
    {
        private Division _division;
        private string _nombre;
        private long _divisionId;

        public virtual Division Division { get => _division; set => _division = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public long DivisionId { get => _divisionId; set => _divisionId = value; }
    }
}