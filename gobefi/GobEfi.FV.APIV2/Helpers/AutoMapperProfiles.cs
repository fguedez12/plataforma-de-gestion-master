using AutoMapper;
using GobEfi.FV.APIV2.Models.DTOs;
using GobEfi.FV.APIV2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.APIV2.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Vehiculo, VehiculoDTO>()
                .ForMember(x=>x.TipoVehiculo,x=>x.MapFrom(y=>y.Modelo.Carroceria))
                .ForMember(x => x.Marca, x => x.MapFrom(y => y.Modelo.Marca))
                .ForMember(x => x.Modelo, x => x.MapFrom(y => y.Modelo.Modelo))
                .ForMember(x => x.KilometrosPorLitro, x => x.MapFrom(y => y.Modelo.Rendimiento_ciudad));
            CreateMap<VehiculoDTO, Vehiculo>();
            CreateMap<VehiculoCreacionDTO,Vehiculo>()
                .ForMember(x=>x.ModeloId,x=>x.MapFrom(y=>y.ModeloIdApi));
            CreateMap<ModeloCreacionDTO, ModeloEm>()
                .ForMember(x=>x.IdEm, x=>x.MapFrom(y=>y.Id))
                .ForMember(x=>x.Id, options=>options.Ignore());
            CreateMap<ModeloEm, ModeloDTO>();
           
            CreateMap<Imagen, ImagenDTO>().ReverseMap();
            CreateMap<VehiculoPatchDTO, Vehiculo>().ReverseMap();
            CreateMap<MenuDTO, Menu>().ReverseMap();
            CreateMap<PermisoDTO, Permiso>().ReverseMap();


        }

        
    }
}
