using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.DTOs.UnidadesDTO;
using GobEfi.Web.Models.UnidadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class UnidadProfile : Profile
    {
        public UnidadProfile()
        {
            CreateMap<UnidadModel, Unidad>();
            CreateMap<Unidad, UnidadDTO>()
                .ForMember(x => x.Inmuebles, opt => opt.MapFrom(x => x.UnidadInmuebles.Select(y => y.Inmueble).ToList()))
                .ForMember(x => x.Pisos, opt => opt.MapFrom(x => x.UnidadPisos.Select(y => y.Piso).ToList()))
                .ForMember(x => x.Areas, opt => opt.MapFrom(x => x.UnidadAreas.Select(y => y.Area).ToList()))
                .ForMember(x => x.InstitucionNombre, opt => opt.MapFrom(x => x.Servicio.Institucion.Nombre))
                .ForMember(x => x.ServicioNombre, opt => opt.MapFrom(x => x.Servicio.Nombre));
                ;
            CreateMap<Unidad, UnidadListDTO>()
                .ForMember(x => x.Ubicacion, options => options.MapFrom(x=> MapUbicacion(x.UnidadAreas, x.UnidadInmuebles, x.UnidadPisos)))
                .ForMember(x => x.Active, options => options.MapFrom(x => MapActivo(x.Active)))
                .ForMember(x=>x.InstitucionId,options=>options.MapFrom(x=>x.Servicio.InstitucionId))
                .ForMember(x => x.InstitucionNombre, opt => opt.MapFrom(x => x.Servicio.Institucion.Nombre))
                .ForMember(x => x.ServicioNombre, opt => opt.MapFrom(x => x.Servicio.Nombre));
            ;
            CreateMap<Division, InmuebleTopDTO>()
                .ForMember(x => x.Calle, options => options.MapFrom(x => x.DireccionInmueble.Calle))
                .ForMember(x => x.Numero, options => options.MapFrom(x => x.DireccionInmueble.Numero))
                .ForMember(x => x.Comuna, options => options.MapFrom(x => x.DireccionInmueble.Comuna.Nombre))
                .ForMember(x => x.Region, options => options.MapFrom(x => x.DireccionInmueble.Region.Nombre))
                ;
            CreateMap<Piso, pisoDTO>()
                .ForMember(x => x.NumeroPisoNombre, opt => opt.MapFrom(x => x.NumeroPiso.Nombre))
                ;

            CreateMap<Area, AreaDTO>();
            CreateMap<UnidadToUpdate, Unidad>();
            CreateMap<Unidad,UnidadToAssociate>()
                .ForMember(x=>x.DireccionInmueble, options=>options.MapFrom(x=>x.UnidadInmuebles.FirstOrDefault().Inmueble.DireccionInmueble.DireccionCompleta))
                .ForMember(x => x.NombreInmueble, options => options.MapFrom(x => x.UnidadInmuebles.Where(ui=>ui.Inmueble.TipoInmueble==2).FirstOrDefault().Inmueble.Nombre))
                .ForMember(x => x.NombreUnidad, options => options.MapFrom(x => x.Nombre))
                .ForMember(x => x.NombreRegion, options => options.MapFrom(x => x.UnidadInmuebles.FirstOrDefault().Inmueble.DireccionInmueble.Region.Nombre))
                ;
        }

        private string MapActivo(bool Activo)
        {
            var result = "No";
            if (Activo) {
                result = "Si";
            }
            return result;
        }

        private string MapUbicacion(List<UnidadArea> unidadAreas, List<UnidadInmueble> unidadInmuebles, List<UnidadPiso> unidadPisos) 
        {
            var result = string.Empty;
            var arrAreas = new List<string>();
            var arrInmuebles = new List<string>();
            var arrPisos = new List<string>();
            if (unidadAreas != null) {
                foreach (var unidadArea in unidadAreas)
                {
                    arrAreas.Add(unidadArea.Area.Nombre);
                }
            }

            if (unidadInmuebles != null)
            {
                foreach (var unidadInmueble in unidadInmuebles)
                {
                    arrInmuebles.Add(unidadInmueble.Inmueble.Nombre);
                }
            }

            if (unidadPisos != null)
            {
                foreach (var unidadPiso in unidadPisos)
                {
                    arrPisos.Add(unidadPiso.Piso.NumeroPiso.Nombre);
                }
            }

            result = string.Join(' ', arrInmuebles);
            if(arrPisos.Count() > 0)
            {
                result = result + " - " + string.Join(' ', arrPisos);
            }
            if (arrAreas.Count() > 0)
            {
                result = result + " - " + string.Join(' ', arrAreas);
            }
            
            return result;
        }
    }
}
