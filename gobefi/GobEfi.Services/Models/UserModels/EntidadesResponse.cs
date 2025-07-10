using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Services.Models.UserModels
{
    public class EntidadesResponse
    {
        public bool Ok { get; set; }
        public EntidadesModel Entidades { get; set; }
        public string Message { get; set; }
    }
}
