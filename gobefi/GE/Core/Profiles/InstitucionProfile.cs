using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.InstitucionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class InstitucionProfile : Profile
    {
        public InstitucionProfile()
        {
            CreateMap<Institucion, InstitucionModel>();
            CreateMap<InstitucionModel, Institucion>();

            CreateMap<Institucion, InstitucionIndexModel>();
        }
    }
}
