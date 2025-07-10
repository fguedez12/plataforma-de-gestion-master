using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.InerciaTermicaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class InerciaTermicaProfile : Profile
    {
        public InerciaTermicaProfile()
        {
            CreateMap<InerciaTermica, InerciaTermicaModel>();
            CreateMap<InerciaTermicaModel, InerciaTermica>();
        }
    }
}
