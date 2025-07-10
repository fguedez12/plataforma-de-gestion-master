using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.DisenioPasivoModels;
using GobEfi.Web.Models.EdificioModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class EdificioProfile : Profile
    {
        public EdificioProfile()
        {
            CreateMap<Edificio, EdificioModel>();
            CreateMap<Edificio, EdificioVerModel>();
            CreateMap<EdificioModel, Edificio>();
            CreateMap<EdificioCreateModel, Edificio>();

            CreateMap<Edificio, EdificioSelectModel>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(x => x.Direccion));

            CreateMap<Edificio, PasoUnoForSave>()
                .ForMember(dest=>dest.TipoAgrupamientoId, opt=>opt.MapFrom(x=>x.TipoAgrupamientoId))
                .ForMember(dest => dest.EntornoId, opt => opt.MapFrom(x => x.EntornoId))
                .ForMember(dest => dest.InerciaTermicaId, opt => opt.MapFrom(x => x.InerciaTermicaId))
                .ForMember(dest => dest.Latitud, opt => opt.MapFrom(x => x.Latitud))
                .ForMember(dest => dest.Longitud, opt => opt.MapFrom(x => x.Longitud));
            CreateMap<Edificio, PasoUnoData>()
                .ForMember(dest => dest.DpSt1, opt => opt.Ignore())
                .ForMember(dest => dest.DpSt2, opt => opt.Ignore())
                .ForMember(dest => dest.DpSt3, opt => opt.Ignore())
                .ForMember(dest => dest.DpSt4, opt => opt.Ignore());
        }
    }
}
