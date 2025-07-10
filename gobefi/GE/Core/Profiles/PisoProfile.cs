using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.MuroModels;
using GobEfi.Web.Models.PisoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class PisoProfile : Profile
    {
        public PisoProfile()
        {
            CreateMap<Piso, PisoModel>()
                .ForMember(dest=>dest.NumeroPisoId, opt=>opt.MapFrom(x=>x.NumeroPiso.Id))
                .ForMember(dest => dest.NumeroPisoNombre, opt => opt.MapFrom(x => x.NumeroPiso.Nombre))
                .ForMember(dest => dest.PisoNumero, opt => opt.MapFrom(x => x.NumeroPiso.Numero))
                .ForMember(dest => dest.TipoNivelId, opt => opt.MapFrom(x => x.NumeroPiso.Nivel))
                .ForMember(dest=>dest.UnidadesPisos,opt=>opt.MapFrom(x=>x.UnidadPisos));
            CreateMap<PisoModel, Piso>();
            CreateMap<PisoForSaveModel, Piso>();
            CreateMap<Piso, PisoForSaveModel>();
            CreateMap<Piso, PisoPasoUnoModel>()
                .ForMember(dest => dest.NumeroPisoId, opt => opt.MapFrom(x => x.NumeroPiso.Id))
                .ForMember(dest => dest.PisoNumero, opt => opt.MapFrom(x => x.NumeroPiso.Numero))
                .ForMember(dest => dest.NumeroPisoNombre, opt => opt.MapFrom(x => x.NumeroPiso.Nombre))
                .ForMember(dest => dest.TipoNivelId, opt => opt.MapFrom(x => x.NumeroPiso.Nivel));
            CreateMap<Piso, PisoPasoDosModel>()
                .ForMember(dest => dest.NumeroPisoId, opt => opt.MapFrom(x => x.NumeroPiso.Id))
                .ForMember(dest => dest.PisoNumero, opt => opt.MapFrom(x => x.NumeroPiso.Numero))
                .ForMember(dest => dest.NumeroPisoNombre, opt => opt.MapFrom(x => x.NumeroPiso.Nombre))
                .ForMember(dest => dest.TipoNivelId, opt => opt.MapFrom(x => x.NumeroPiso.Nivel));
        }
    }
}
