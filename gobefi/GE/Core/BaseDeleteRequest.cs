using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core
{
    public class BaseDeleteRequest
    {
        public int Id { set; get; } = 1;
        public int ProyectoId { set; get; } = 10;
        public string Token { set; get; }
    }
}
