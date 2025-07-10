using GobEfi.FV.APIV2.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.APIV2.Services
{
    public interface IElectroMobilidadService
    {
        Task<List<ModeloDTO>> BuscarModelo(BuscarModeloDTO buscar);
        Task<List<string>> GetCarroceria();
        Task<List<string>> GetMarca();
        Task<List<string>> GetPropulsion();
    }
}
