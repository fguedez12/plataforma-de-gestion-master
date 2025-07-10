using GobEfi.Web.Models.MedidorDivisionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IMedidorDivisionService : IService<MedidorDivisionSwitchModel, long>
    {
        void DeshabilitarDeDivision(long numeroClienteId, long divisionId);
        void Delete(long medidorId, long divisionId);
    }
}
