using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.TechoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class TechoProfile : Profile
    {
        public TechoProfile()
        {
            CreateMap<Techo, TechoModel>();
            CreateMap<TechoModel, Techo>();
        }
    }
}
