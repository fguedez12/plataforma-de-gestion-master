using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class TipoPuerta
    {
        private long _id;
        private string _nombre;
        private double _factor;

        public string Nombre { get => _nombre; set => _nombre = value; }
        public double Factor { get => _factor; set => _factor = value; }
        public long Id { get => _id; set => _id = value; }
    }
}
