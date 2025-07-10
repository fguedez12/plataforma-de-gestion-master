using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.DTOs.InmuebleDTO;
using GobEfi.Web.Models.InmuebleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class InmuebleProfile : Profile
    {
        public InmuebleProfile()
        {
            CreateMap<Division , InmuebleModel>()
                .ForMember(dest => dest.RegionId, opt => opt.MapFrom(x => x.DireccionInmueble.RegionId))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(x => x.DireccionInmueble.Region.Nombre))
                .ForMember(dest => dest.ComunaId, opt => opt.MapFrom(x => x.DireccionInmueble.ComunaId))
                .ForMember(dest => dest.Comuna, opt => opt.MapFrom(x => x.DireccionInmueble.Comuna.Nombre))
                .ForMember(dest => dest.Calle, opt => opt.MapFrom(x => x.DireccionInmueble.Calle))
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(x => x.DireccionInmueble.Numero))
                .ForMember(dest=>dest.AdministracionServicioId, opt=>opt.MapFrom(x=>x.AdministracionServicio.Id))
                .ForMember(dest => dest.AdministracionServicioNombre, opt => opt.MapFrom(x => x.AdministracionServicio.Nombre))
                .ForMember(dest => dest.AdministracionMinisterioId, opt => opt.MapFrom(x => x.AdministracionServicio.Institucion.Id))
                .ForMember(dest => dest.AdministracionInstitucionNombre, opt => opt.MapFrom(x => x.AdministracionServicio.Institucion.Nombre))
                .ForMember(dest=>dest.UnidadesInmuebles,opt=>opt.MapFrom(x=>x.UnidadInmuebles))
                .ForMember(dest=>dest.Pisos, opt=>opt.Ignore())
                ; 
            CreateMap<InmuebleModel, Division>();
            CreateMap<InmuebleToUpdateRequest, Division>().ReverseMap();
            CreateMap<Division, InmuebleListDTO>()
                .ForMember(x => x.TipoInmueble, opt => opt.MapFrom(t=> GetTipoInmueble(t)))
                .ForMember(x=>x.Direccion, opt=>opt.MapFrom(t=>t.DireccionInmueble.DireccionCompleta));
        }

        private string GetTipoInmueble(Division division)
        {
            return division.TipoInmueble == 1 ? "Complejo" : "Edificio";
        }
    }
}
