using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Servicio : BaseEntity
    {
        private string _identificador;
        private long _institucionId;
        private string _nombre;
        private bool _reportaPMG;
        private int _oldId;
        private bool _CompraActiva;
        private Institucion _institucion;
        public bool GEV3 { get; set; }
        public string Identificador { get => _identificador; set => _identificador = value; }
        public long InstitucionId { get => _institucionId; set => _institucionId = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public bool ReportaPMG { get => _reportaPMG; set => _reportaPMG = value; }
        public virtual Institucion Institucion { get => _institucion; set => _institucion = value; }
        public int OldId { get => _oldId; set => _oldId = value; }
        public bool CompraActiva { get=> _CompraActiva; set=>_CompraActiva = value; }
        public string Justificacion { get; set; }
        public bool RevisionRed { get; set; } = false;
        public bool? NoDeclaraViajeAvion { get; set; }
        public bool? NoRegistraPoliticaAmbiental { get; set; }
        public bool? NoRegistraDifusionInterna { get; set; }
        public bool? NoRegistraActividadInterna { get; set; }
        public bool? NoRegistraReutilizacionPapel { get; set; }
        public bool? NoRegistraProcFormalPapel { get; set; }
        public bool? NoRegistraDocResiduosCertificados { get; set; }
        public bool? NoRegistraDocResiduosSistemas { get; set; }
        public bool? NoRegistraProcBajaBienesMuebles{ get; set; }
        public bool? NoRegistraProcComprasSustentables{ get; set; }
        public string ComentarioRed { get; set; }
        public bool RevisionDiagnosticoAmbiental { get; set; }
        public bool ModificacioAlcance { get; set; }
        public int ColaboradoresModAlcance { get; set; }
        public int EtapaSEV { get; set; }
        public bool BloqueoIngresoInfo { get; set; }
        public bool? NoDeclaraViajeAvion2025 { get; set; }
        public bool? PgaRevisionRed { get; set; }
        public string PgaObservacionRed{ get; set; }
        public string PgaRespuestaRed{ get; set; }
        public bool? ValidacionConcientizacion { get; set; }
        public virtual ICollection<UsuarioServicio> UsuariosServicios { get; set; }
        public ICollection<MedidorInteligenteServicio> MedidorInteligenteServicios { get; set; }
        public List<Unidad> Unidades { get; set; }
        public List<Documento> Documentos { get; set; }
        public List<Viaje> Viajes { get; set; }
        public List<Brecha> Brechas { get; set; }
        public ICollection<DimensionServicio> DimensionServicios { get; set; }
        public ICollection<AccionServicio> AccionServicios { get; set; }
        public ICollection<Programa> Programas { get; set; }

    }
}
