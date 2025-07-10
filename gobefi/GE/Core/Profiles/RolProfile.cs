using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.RolModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class RolProfile : Profile
    {
        public RolProfile()
        {
            CreateMap<Rol, RolModel>();
            CreateMap<RolModel, Rol>();
        }
    }
}
