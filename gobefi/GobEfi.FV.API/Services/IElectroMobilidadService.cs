using GobEfi.FV.API.Models.DTOs;
using GobEfi.FV.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Services
{
    public interface IElectroMobilidadService
    {
        Task<List<ModeloDTO>> BuscarModelo(BuscarModeloDTO buscar);
        Task<List<string>> GetCarroceria();
        Task<List<string>> GetMarca();
        Task<List<string>> GetPropulsion();
    }
}
