using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.InmuebleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class DireccionProfile : Profile
    {
        public DireccionProfile()
        {
          CreateMap<DireccionModel, Direccion>().ReverseMap();
        }
    }
}
