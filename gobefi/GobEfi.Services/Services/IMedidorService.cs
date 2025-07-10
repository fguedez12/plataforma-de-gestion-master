using GobEfi.Services.Models;
using GobEfi.Services.Models.MedidoresModels;
using GobEfi.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Services.Services
{
    public interface IMedidorService
    {
        Task<MedidorInteligente> Create(MedidorModel medidor);
        Task<List<UnidadModel>> GetByUnidades(long[] ids);
    }
}
