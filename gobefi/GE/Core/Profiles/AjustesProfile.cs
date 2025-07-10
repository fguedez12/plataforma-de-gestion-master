using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.DTOs.AjustesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class AjustesProfile : Profile
    {
        public AjustesProfile()
        {
            CreateMap<AjustesDTO, Ajuste>().ReverseMap();
        }
    }
}
