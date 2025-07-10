using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.DisenioPasivoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class ArchivoDpProfile : Profile
    {
        public ArchivoDpProfile()
        {
            CreateMap<ArchivoDp, ArchivoDpModel>()
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(x => x.CreatedAt));
            CreateMap<ArchivoDpModel, ArchivoDp>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(x => x.Fecha)); ;
            CreateMap<ArchivoDp, ArchivoPasoCuatroModel>()
               .ForMember(dest => dest.Fecha, opt => opt.MapFrom(x => x.CreatedAt));
        }
    }
}
