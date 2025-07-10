using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class UsuarioDivision
    {
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public long DivisionId { get; set; }
        public Division Division { get; set; }
    }
}
