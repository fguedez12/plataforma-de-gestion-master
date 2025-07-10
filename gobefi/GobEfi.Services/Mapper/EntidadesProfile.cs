using AutoMapper;
using GobEfi.Services.Models.UserModels;
using GobEfi.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Services.Mapper
{
    public class EntidadesProfile : Profile
    {
        public EntidadesProfile()
        {
            CreateMap<Usuario, EntidadesModel>()
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(x => x.Active));
            CreateMap<Rol, RolesModel>();
            CreateMap<Institucion, InstitucionModel>();
            CreateMap<Servicio, ServicioModel>();
            CreateMap<Division, DivisionModel>();
        }
    }
}
