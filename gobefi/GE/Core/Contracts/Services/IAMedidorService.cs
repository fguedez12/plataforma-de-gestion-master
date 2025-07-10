using GobEfi.Web.Models.AMedidorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IAMedidorService
    {
        Task<MedidorPaginModel> ListMedidores(int? filtroServicio , int page,int? id);
        Task UpdateMedidor(MedidorModel model);
    }
}
