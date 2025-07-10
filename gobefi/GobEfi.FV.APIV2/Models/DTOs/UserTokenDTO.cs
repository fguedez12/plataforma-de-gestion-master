using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.APIV2.Models.DTOs
{
    public class UserTokenDTO
    {
        public bool Ok { get; set; }
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
    }
}
