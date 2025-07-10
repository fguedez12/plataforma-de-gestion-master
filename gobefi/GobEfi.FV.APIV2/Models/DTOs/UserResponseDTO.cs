using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.APIV2.Models.DTOs
{
    public class UserResponseDTO
    {
        public bool Ok { get; set; }
        public UserInfoDTO User { get; set; }
    }
}
