using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.DTOs.AreaDTO;
using GobEfi.Web.Models.AreaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class AreaProfile : Profile
    {
        public AreaProfile()
        {
            CreateMap<AreaModel,Area>();
            CreateMap<Area, AreaModel>()
                .ForMember(dest => dest.UnidadesAreas, opt => opt.MapFrom(x => x.UnidadAreas));
            CreateMap<AreaEditDTO, Area>();
        }
    }
}
