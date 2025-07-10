using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class ModoOperacion : BaseEntity
    {
        private string _nombre;

        public string Nombre { get => _nombre; set => _nombre = value; }
    }
}
