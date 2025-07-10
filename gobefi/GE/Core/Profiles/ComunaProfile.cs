using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.ComunaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class ComunaProfile : Profile
    {
        public ComunaProfile()
        {
            CreateMap<Comuna, ComunaModel>();
            CreateMap<ComunaModel, Comuna>();
        }
    }
}
