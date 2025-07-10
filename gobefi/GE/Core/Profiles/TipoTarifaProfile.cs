using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.TipoTarifaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class TipoTarifaProfile : Profile
    {
        public TipoTarifaProfile()
        {
            CreateMap<TipoTarifa, TipoTarifaModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(x => x.Nombre));
                

            CreateMap<TipoTarifa, TipoTarifaModel>();
            CreateMap<TipoTarifaModel, TipoTarifa>();
        }
    }
}

