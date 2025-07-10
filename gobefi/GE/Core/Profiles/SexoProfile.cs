using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.SexoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class SexoProfile : Profile
    {
        public SexoProfile()
        {
            CreateMap<Sexo, SexoModel>();
            CreateMap<SexoModel, Sexo>();
            
        }
    }
}
