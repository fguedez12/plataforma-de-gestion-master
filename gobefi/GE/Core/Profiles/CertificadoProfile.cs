using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.CertificadoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class CertificadoProfile : Profile
    {
        public CertificadoProfile()
        {
            CreateMap<Certificado, CertificadoModel>();
            CreateMap<CertificadoModel, Certificado>();
            CreateMap<NotasCertificado, NotaModel>()
               .ForMember(dest => dest.NombreCertificado, opt => opt.MapFrom(x => x.Certificado.Nombre));
           

            CreateMap<NotaModel, NotasCertificado>();

            CreateMap<Servicio,ServicioToListModel>()
              .ForMember(dest => dest.MinisterioId, opt => opt.MapFrom(x => x.InstitucionId));

        }
    }
}
