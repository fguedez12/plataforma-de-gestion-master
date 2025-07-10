using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.UnidadMedidaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class UnidadMedidaProfile : Profile
    {
        public UnidadMedidaProfile()
        {
            CreateMap<UnidadMedida, UnidadMedidaModel>()
                .ForMember(dest => dest.Abreviacion, opt => opt.MapFrom(x => x.Abrv));
        }

    }
}
