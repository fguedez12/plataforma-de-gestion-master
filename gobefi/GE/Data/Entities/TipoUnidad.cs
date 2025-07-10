using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class TipoUnidad
    {
        private long _id;
        private string _nombre;
        private long _oldId;

        public string Nombre { get => _nombre; set => _nombre = value; }
        public long Id { get => _id; set => _id = value; }
        public long OldId { get => _oldId; set => _oldId = value; }
    }
}