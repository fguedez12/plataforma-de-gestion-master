using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Models.DTOs
{
    public class VehiculoPostDTO
    {
        public VehiculoCreacionDTO Vehiculo { get; set; }
        public IFormFile File { get; set; }
    }
}
