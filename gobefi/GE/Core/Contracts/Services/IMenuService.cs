using System.Collections.Generic;
using GobEfi.Web.Models.MenuModels;
using GobEfi.Web.Models.MenuPanelModels;
using GobEfi.Web.Core.Contracts.Services;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IMenuService : IService<MenuModel, long>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ICollection<MenuPanelModel> GetByUser(string userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rutaActual"></param>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        Task<ICollection<MenuModel>> GetSubMenusByMenuAndRoles(string rutaActual, string usuarioId, long? servicioIdEv = null);
    }
}