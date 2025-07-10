using GobEfi.FV.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Services
{
    public interface ISectorPublicoService
    {
        Task<UserInfoDTO> GetUser(UserDTO userDTO);
    }
}
