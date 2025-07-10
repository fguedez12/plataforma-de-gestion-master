using GobEfi.Web.Models.TipoSombreadoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface ITipoSombreadoService
    {
        Task<List<TipoSombreadoModel>> Get();
    }
}
