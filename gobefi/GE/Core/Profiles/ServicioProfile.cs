using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.DTOs.UnidadesDTO;
using GobEfi.Web.Models.ServicioModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class ServicioProfile : Profile
    {
        public ServicioProfile()
        {
            CreateMap<Servicio, ServicioModel>();
            CreateMap<ServicioModel, Servicio>();
            CreateMap<Servicio, ServicioDTO>();
        }
    }
}
