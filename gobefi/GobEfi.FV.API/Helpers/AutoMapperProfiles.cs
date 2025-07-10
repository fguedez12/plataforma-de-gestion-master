using AutoMapper;
using GobEfi.FV.API.Models.DTOs;
using GobEfi.FV.API.Models.Entities;
using GobEfi.FV.Shared.DTOs;
using GobEfi.FV.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Vehiculo, VehiculoDTO>()
                .ForMember(dest=> dest.TipoVehiculo,opt=>opt.MapFrom(src=>
                    (src.Modelo!=null ? src.Modelo.Carroceria : src.Carroceria)))
                .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => 
                    (src.Modelo!=null ? src.Modelo.Marca : src.Marca)))
                .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => 
                    (src.Modelo!=null ? src.Modelo.Modelo : src.ModeloOtro)))
                .ForMember(x => x.KilometrosPorLitro, x => x.MapFrom(y => y.Modelo.Rendimiento_ciudad));
            CreateMap<VehiculoDTO, Vehiculo>();
            CreateMap<VehiculoCreacionDTO, Vehiculo>();
            CreateMap<ModeloCreacionDTO, ModeloEm>()
                .ForMember(x=>x.IdEm, x=>x.MapFrom(y=>y.Id))
                .ForMember(x=>x.Id, options=>options.Ignore());
            CreateMap<ModeloEm, ModeloDTO>();
           
            CreateMap<Imagen, ImagenDTO>().ReverseMap();
            CreateMap<VehiculoPatchDTO, Vehiculo>().ReverseMap();
            CreateMap<MenuDTO, Menu>().ReverseMap();
            CreateMap<PermisoDTO, Permiso>().ReverseMap();
            CreateMap<ModeloEm, ModeloSearchDTO>();
            CreateMap<Propulsion, PropulsionDTO>();
            CreateMap<Combustible, CombustibleDTO>();


        }

        
    }
}
