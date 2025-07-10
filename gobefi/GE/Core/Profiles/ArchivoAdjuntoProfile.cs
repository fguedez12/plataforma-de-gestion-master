using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.ArchivoAdjuntoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class ArchivoAdjuntoProfile : Profile
    {
        public ArchivoAdjuntoProfile()
        {
            CreateMap<ArchivoAdjunto, ArchivoAdjuntoForRegister>();
            CreateMap<ArchivoAdjuntoForRegister, ArchivoAdjunto>();

            CreateMap<ArchivoAdjuntoForEdit, ArchivoAdjunto>();
            CreateMap<ArchivoAdjunto, ArchivoAdjuntoForEdit>();

        }
    }
}
