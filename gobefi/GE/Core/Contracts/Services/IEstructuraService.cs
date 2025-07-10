using GobEfi.Web.Models.EstructuraModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IEstructuraService
    {
        Task<ICollection<EstructuraModel>> GetAll();
        Task<List<EspesorModel>> GetEspesores();
    }
}
