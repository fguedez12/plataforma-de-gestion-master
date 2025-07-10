using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EstructuraModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class EstructuraProfile : Profile
    {
        public EstructuraProfile()
        {
            CreateMap<EstructuraModel, Estructura>();
            CreateMap<Estructura, EstructuraModel>();

        }
    }
}
