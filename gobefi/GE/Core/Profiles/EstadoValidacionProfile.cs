using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EstadoValidacionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class EstadoValidacionProfile : Profile
    {
        public EstadoValidacionProfile() {

            CreateMap<EstadoValidacion, EstadoValidacionModel>();
        }

       
    }
}
