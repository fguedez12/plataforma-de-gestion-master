using GobEfi.FV.APIV2.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.APIV2.Services
{
    public interface ISectorPublicoService
    {
        Task<UserInfoDTO> GetUser(UserDTO userDTO);
    }
}
