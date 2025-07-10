using api_gestiona.DTOs;
using api_gestiona.DTOs.Agua;
using api_gestiona.DTOs.Ajustes;
using api_gestiona.DTOs.Artefactos;
using api_gestiona.DTOs.Compras;
using api_gestiona.DTOs.Contenedores;
using api_gestiona.DTOs.Divisiones;
using api_gestiona.DTOs.Documentos;
using api_gestiona.DTOs.Edificios;
using api_gestiona.DTOs.EncuestaColaborador;
using api_gestiona.DTOs.Impresoras;
using api_gestiona.DTOs.Instituciones;
using api_gestiona.DTOs.Medidor;
using api_gestiona.DTOs.PlanGestion;
using api_gestiona.DTOs.RegistrosDTOs;
using api_gestiona.DTOs.Residuos;
using api_gestiona.DTOs.Resmas;
using api_gestiona.DTOs.Servicios;
using api_gestiona.DTOs.Sistemas;
using api_gestiona.DTOs.TipoDocumentos;
using api_gestiona.DTOs.TipoUsoListDTO;
using api_gestiona.DTOs.UserDTO;
using api_gestiona.DTOs.Viajes;
using api_gestiona.Entities;
using AutoMapper;
using Microsoft.Graph.Models;

namespace api_gestiona.Helpers
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Registro, RegistroListDTO>();
            CreateMap<TipoUso, TipoUsoListDTO>().ReverseMap();
            CreateMap<Institucion, InstitucionListDTO>();
            CreateMap<TipoPropiedad, TipoPropiedadDTO>();
            CreateMap<Servicio, ServicioDTO>();
            CreateMap<Region, RegionDTO>();
            CreateMap<Comuna, ComunaDTO>();
            CreateMap<TipoDocumento, TipoDocumentoDTO>();
            CreateMap<ActaDTO, ActaComite>();
            CreateMap<ActaComite, ActaDTO>()
                .ForMember(dest=>dest.TipoDocumentoNombre , x=>x.MapFrom(a=>a.TipoDocumento.Nombre));
            CreateMap<ReunionDTO, ReunionComite>().ReverseMap();
            CreateMap<Documento, ComiteDTO>()
                .ForMember(dest => dest.TipoDocumentoNombre, x => x.MapFrom(a => a.TipoDocumento.Nombre))
                .ForMember(dest => dest.TipoDocumentoNombreE2, x => x.MapFrom(a => a.TipoDocumento.NombreE2)); ;
            CreateMap<ListaIntegrantesDTO, ListaIntegrante>().ReverseMap();
            CreateMap<PoliticaDTO, Politica>().ReverseMap();
            CreateMap<Documento, PoliticaListaDTO>()
                .ForMember(dest => dest.TipoDocumentoNombre, x => x.MapFrom(a => a.EtapaSEV_docs == 2 ? a.TipoDocumento.NombreE2 : a.TipoDocumento.Nombre))
                .ForMember(dest=>dest.NResolucionPolitica, x=>x.MapFrom(a=>a.NResolucionPolitica));
            CreateMap<DifusionDTO, DifusionPolitica>().ReverseMap();
            CreateMap<ProcedimientoPapelDTO,ProcedimientoPapel>().ReverseMap();
            CreateMap<Documento, ProcedimientoListaDto>()
                .ForMember(dest => dest.TipoDocumentoNombre, x => x.MapFrom(a => a.TipoDocumento.Nombre));
            CreateMap<ProcedimientoResiduoDTO, ProcedimientoResiduo>().ReverseMap();
            CreateMap<ProcedimientoResiduoSistemaDTO, ProcedimientoResiduoSistema>().ReverseMap();
            CreateMap<ProcedimientoBajaBienesDTO, ProcedimientoBajaBienes>().ReverseMap();
            CreateMap<ProcedimientoCompraSustentableDTO, ProcedimientoCompraSustentable>().ReverseMap();
            CreateMap<CharlasDTO,Charla>().ReverseMap();
            CreateMap<Documento, CharlaListaDTO>()
                .ForMember(dest => dest.TipoDocumentoNombre, x => x.MapFrom(a => a.EtapaSEV_docs == 2 ? a.TipoDocumento.NombreE2 : a.TipoDocumento.Nombre));
            CreateMap<DivisionDTO, Division>().ReverseMap();
            CreateMap<ImpresoraDTO, Impresora>().ReverseMap();
            CreateMap<ResmaDTO, Resma>().ReverseMap();
            CreateMap<AguaDTO, Agua>().ReverseMap();
            CreateMap<ArtefactoDTO, Artefacto>().ReverseMap()
                 .ForMember(dest => dest.TipoArtefacto, x => x.MapFrom(a => a.TipoArtefacto.Nombre)); 
            CreateMap<TipoArtefactosDTO, TipoArtefacto>().ReverseMap();
            CreateMap<ResiduoDTO, Residuo>().ReverseMap();
            CreateMap<ContenedorDTO, Contenedor>().ReverseMap();
            CreateMap<IntegranteDTO, Integrante>().ReverseMap();
            CreateMap<ListadoColaboradorDTO, ListadoColaborador>().ReverseMap();
            CreateMap<EncuestaColaborador, EncuestaColaboradorResponseDTO>();
            CreateMap<EncuestaColaborador, VehiculoPropioDTO>();
            CreateMap<EncuestaColaborador, VehiculoCompartidoDTO>();
            CreateMap<EncuestaColaborador, TransportePublicoDTO>();
            CreateMap<EncuestaColaborador, BicicletaDTO>();
            CreateMap<EncuestaColaborador, MotocicletaDTO>();
            CreateMap<EncuestaColaborador, OtrasFormasDTO>();
            CreateMap<ViajeCreateDTO, Viaje>().ReverseMap();
            CreateMap<Viaje, ViajeListDTO>()
                .ForMember(dest=>dest.AeropuertoOrigen, x=>x.MapFrom(a=>a.AeropuertoOrigen.Nombre))
                .ForMember(dest => dest.AeropuertoDestino, x => x.MapFrom(a => a.AeropuertoDestino.Nombre));
            CreateMap<Viaje, ViajeEditDTO>()
                .ForMember(dest => dest.PaisOrigenId, x => x.MapFrom(a => a.AeropuertoOrigen.Pais.Id))
                .ForMember(dest => dest.PaisDestinoId, x => x.MapFrom(a => a.AeropuertoDestino.Pais.Id))
                ;
            CreateMap<Documento, ProcedimientoListaDTO>().ReverseMap();
            CreateMap<ProcReutilizacionPapelDTO, ProcedimientoReutilizacionPapel>().ReverseMap();
            CreateMap<Servicio,ServicioPatchDTO>().ReverseMap();
            CreateMap<Edificio, EdificioListDTO>()
                .ForMember(dest=>dest.Direccion, x=> x.MapFrom(a=>a.Direccion+", "+a.Comuna.Nombre))
                .ForMember(dest => dest.RegionId, x => x.MapFrom(a => a.Comuna.Region.Id))
                .ForMember(dest => dest.Region, x => x.MapFrom(a => a.Comuna.Region.Nombre))
                .ForMember(dest => dest.ComunaId, x => x.MapFrom(a => a.Comuna.Id))
                .ForMember(dest => dest.Comuna, x => x.MapFrom(a => a.Comuna.Nombre))
                ;
            CreateMap<Servicio, ServicioListDTO>();
            CreateMap<Division, ActualizacionDivisionDTO>()
                .ForMember(dest => dest.VehiculosIds, m => m.MapFrom(src => src.VehiculosIds.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()));
            CreateMap<Division, DivisionPatchDTO>();
                //.ForMember(dest=>dest.VehiculosIds,m => m.MapFrom(src => src.VehiculosIds.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()));
            CreateMap<DivisionPatchDTO, Division>();
            CreateMap<Ajuste, AjustesDTO>();
            CreateMap<Medidor,MedidorDTO>();
            CreateMap<Usuario,UserDTO>();
            CreateMap<Pais, PaisListaDTO>();
            CreateMap<Aeropuerto, AeropuertoListaDTO>();
            CreateMap<CapacitadosMPDTO, CapacitadosMP>().ReverseMap();
            CreateMap<PacE3DTO, PacE3>().ReverseMap();
            CreateMap<ResolucionApruebaPlanDTO, ResolucionApruebaPlan>().ReverseMap();
            CreateMap<GestionCompraSustentableDTO, GestionCompraSustentable>().ReverseMap();
            CreateMap<Servicio,DiagnosticoDTO>();
            CreateMap<Division, SistemasDataDTO>();
            CreateMap<TipoLuminaria, LuminariasListDTO>();
            CreateMap<TipoEquipoCalefaccion, EquiposCalefaccionListDTO>();
            CreateMap<Energetico,EnergeticoEquipoListDTO>();
            CreateMap<TipoColector, TipoColectorListDTO>();
            CreateMap<SistemasDataDTO, Division>();
            CreateMap<DimensionBrecha, DimensionDTO>();
            CreateMap<DimensionDTO, DimensionServicio>()
                .ForMember(dest=>dest.DimensionBrechaId, m=>m.MapFrom(x=>x.Id));
            CreateMap<Division, DivisionListDTO>();
            CreateMap<BrechaToSaveDTO, Brecha>()
                .ForMember(dest=>dest.DimensionBrechaId,m=>m.MapFrom(x=>x.DimensionId));
            CreateMap<DivisionListDTO, BrechaUnidad>()
                .ForMember(dest => dest.DivisionId, m => m.MapFrom(x => x.Id));
            CreateMap<Brecha, BrechaListDTO>();
            CreateMap<Brecha, BrechaToEditDTO>()
                .ForMember(dest=>dest.DimensionId,m=>m.MapFrom(x=>x.DimensionBrechaId)).ReverseMap();
            CreateMap<ObjetivoToSaveDTO, Objetivo>();
            CreateMap<Objetivo, ObjetivoListDTO>()
                .ForMember(dest => dest.TieneBrechaPorUnidad, m => m.MapFrom(x => x.Brechas!.Any(b => b.TipoBrecha == 2) ? true:false));
            CreateMap<Objetivo, ObjetivoToEditDTO>()
                .ForMember(dest=>dest.BrechasSelected,m=>m.MapFrom(x=>x.Brechas));
            CreateMap<ObjetivoToEditDTO, Objetivo>();
            CreateMap<Medida, MedidaListDTO>();
            CreateMap<Usuario, UserListDTO>()
                .ForMember(dest=>dest.Nombre, m=>m.MapFrom(x=>x.Nombres+" "+x.Apellidos));
            CreateMap<AccionToSaveDTO, Accion>();
            CreateMap<Accion, AccionListDTO>()
                .ForMember(dest=>dest.Medida, m=>m.MapFrom(x=> x.Medida != null ? x.Medida.Nombre : string.Empty))
                .ForMember(dest => dest.Tareas, opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    if (context.Items.ContainsKey("IncludeTareas") && (bool)context.Items["IncludeTareas"])
                    {
                        return context.Mapper.Map<List<TareaListDTO>>(src.Tareas);
                    }
                    else
                    {
                        return null;
                    }
                }));
            CreateMap<Accion, AccionToEditDTO>()
                .ForMember(dest => dest.Medida, m => m.MapFrom(x => x.Medida != null ? x.Medida.Nombre : string.Empty));
            CreateMap<IndicadorToSaveDTO, Indicador>();
            CreateMap<Indicador, IndicadorListDTO>();
            CreateMap<Indicador, IndicadorToEditDTO>().ReverseMap();
            CreateMap<ProgramaToSaveDTO, Programa>();
            CreateMap<Programa, ProgramaListDTO>();
            CreateMap<Programa, ProgramaToEditDTO>().ReverseMap();
            CreateMap<CompraToSaveDTO,Compra>();
            CreateMap<InformeDADTO, InformeDA>().ReverseMap();
            CreateMap<PgaListaDTO, Documento>().ReverseMap();
            
            // Mapeos para Tarea
            CreateMap<Tarea, TareaListDTO>()
                .ForMember(dest => dest.DimensionBrechaDescripcion, opt => opt.MapFrom(src => src.DimensionBrecha.Nombre))
                .ForMember(dest => dest.AccionDescripcion, opt => opt.MapFrom(src => src.Accion.MedidaDescripcion));
            CreateMap<TareaToSaveDTO, Tarea>();
            CreateMap<TareaToEditDTO, Tarea>();
        }
    }
}

