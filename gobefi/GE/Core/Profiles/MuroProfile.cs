using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.MuroModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class MuroProfile : Profile
    {
        public MuroProfile()
        {
            CreateMap<MuroModel,Muro>()
                
                .ForMember(dest => dest.Azimut, opt => opt.MapFrom(x => x.Bearing))
                .ForMember(dest => dest.Distancia, opt => opt.MapFrom(x => x.Distance))
                .ForMember(dest => dest.Latitud, opt => opt.MapFrom(x => x.Lat))
                .ForMember(dest => dest.Longitud, opt => opt.MapFrom(x => x.Lng))
                .ForMember(dest => dest.Orientacion, opt => opt.MapFrom(x => x.Orientation))
                .ForMember(dest => dest.TipoMuro, opt => opt.MapFrom(x => x.Tipo))
                ;
            CreateMap<Muro, MuroModel>()
                .ForMember(dest => dest.Bearing, opt => opt.MapFrom(x => x.Azimut))
                .ForMember(dest => dest.Distance, opt => opt.MapFrom(x => x.Distancia))
                .ForMember(dest => dest.Lat, opt => opt.MapFrom(x => x.Latitud))
                .ForMember(dest => dest.Lng, opt => opt.MapFrom(x => x.Longitud))
                .ForMember(dest => dest.Orientation, opt => opt.MapFrom(x => x.Orientacion))
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(x => x.TipoMuro));
            CreateMap<Muro, MuroPasoDosModel>()
                .ForMember(dest => dest.Bearing, opt => opt.MapFrom(x => x.Azimut))
                .ForMember(dest => dest.Distance, opt => opt.MapFrom(x => x.Distancia))
                .ForMember(dest => dest.Lat, opt => opt.MapFrom(x => x.Latitud))
                .ForMember(dest => dest.Lng, opt => opt.MapFrom(x => x.Longitud))
                .ForMember(dest => dest.Orientation, opt => opt.MapFrom(x => x.Orientacion))
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(x => x.TipoMuro));
        }
    }
}
