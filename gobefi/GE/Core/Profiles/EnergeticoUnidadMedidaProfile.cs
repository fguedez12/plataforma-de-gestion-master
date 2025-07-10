using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EnergeticoUnidadMedidaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class EnergeticoUnidadMedidaProfile : Profile
    {

        public EnergeticoUnidadMedidaProfile()
        {
            CreateMap<EnergeticoUnidadMedida, EnergeticoUnidadMedidaModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Calor, opt => opt.MapFrom(x => x.Calor))
                .ForMember(dest => dest.Densidad, opt => opt.MapFrom(x => x.Densidad))
                .ForMember(dest => dest.EnergeticoId, opt => opt.MapFrom(x => x.EnergeticoId))
                .ForMember(dest => dest.Factor, opt => opt.MapFrom(x => x.Factor))
                .ForMember(dest => dest.UnidadMedidaId, opt => opt.MapFrom(x => x.UnidadMedidaId)).ReverseMap();
        }

    }
}
