using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.ServicioModels;
using GobEfi.Web.Services.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IServicioService : IService<ServicioModel, long>
    {
        ServicioVerModel Ver(long id);

        PagedList<ServicioListModel> Paged(ServicioRequest request);

        IEnumerable<ServicioVerModel> Export(ServicioRequest request);

        IEnumerable<ServicioSelectModel> Select();

        Task<ServicioDataModel> GetData(long id);

        Task<IEnumerable<ServicioModel>> GetServiciosByUserIdAsync(string id);

        Task<IEnumerable<ServicioModel>> GetServiciosByInstitucionIdAsync(long id);

        Task<IEnumerable<ServicioListModel>> AllByUserAndInstitucion(string userId, long institucionId);

        Task<IEnumerable<ServicioListModel>> AllByUser(string id);

        void Update(ServicioModel model, long id);

        void Toogle(long id);

        Task<IEnumerable<ServicioListModel>> GetAsociadosByUserId(string userId);
        Task<IEnumerable<ServicioListModel>> GetNoAsociadosByUserId(string userId);
        Task<IEnumerable<ServicioListModel>> GetNoAsociados(string userId, long institucionId);
        Task<IEnumerable<ServicioModel>> GetByInstitucionId(long institucionId);
        Task<IEnumerable<ServicioModel>> GetServicios();
        Task<ServicioModel> GetAsync(long servicioId);
        void Toogle(long id, long divisionId);
        Task<bool> IsGEV3(Usuario user);
        Task<IEnumerable<ServicioModel>> GetByInstitucionIdAndUserId(long institucionId, string userId);
    }
}
