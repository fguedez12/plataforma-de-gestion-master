using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.MedidorDivisionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class MedidorDivisionProfile : Profile
    {
        public MedidorDivisionProfile()
        {
            CreateMap<MedidorDivisionSwitchModel, MedidorDivision>();
            CreateMap<MedidorDivision, MedidorDivisionSwitchModel>();
        }
    }
}
