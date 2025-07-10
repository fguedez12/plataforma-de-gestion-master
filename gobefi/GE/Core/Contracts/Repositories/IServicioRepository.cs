using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.ServicioModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Repositories
{
    public interface IServicioRepository : IRepository<Servicio, long>
    {
        // Task<IEnumerable<ServicioListModel>> AllByUserAndInstitucion(string userId, long institucionId);

        Task<IEnumerable<ServicioListModel>> AllByUser(string id);
    }
}
