using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GobEfi.Web.Data.Entities
{
    public abstract class Documento : BaseEntity
    {
        public DateTime? Fecha { get; set; }
        public string AdjuntoUrl { get; set; }
        public string AdjuntoNombre { get; set; }
        public string AdjuntoRespaldoUrlParticipativo { get; set; }
        public string AdjuntoRespaldoNombreParticipativo { get; set; }
        public string AdjuntoRespaldoUrlCompromiso { get; set; }
        public string AdjuntoRespaldoNombreCompromiso { get; set; }
        public string AdjuntoRespaldoUrl { get; set; }
        public string AdjuntoRespaldoNombre { get; set; }
        public string AdjuntoUrlCompraSustentableAnt { get; set; }
        public string AdjuntoNombreCompraSustentableAnt { get; set; }
        public string AdjuntoUrlCompraFuera { get; set; }
        public string AdjuntoNombreCompraFuera { get; set; }
        [MaxLength(500)]
        public string Observaciones { get; set; }
        public int TipoDocumentoId { get; set; }
        public long? ActaComiteId { get; set; }
        public string Titulo { get; set; }
        public int? Cobertura { get; set; }
        public bool Reduccion { get; set; }
        public bool Reutilizacion { get; set; }
        public bool Reciclaje { get; set; }
        public bool ProdBajoImpactoAmbiental { get; set; }
        public bool ProcesoGestionSustentable { get; set; }
        public bool EstandaresSustentabilidad { get; set; }
        public long? PoliticaId { get; set; }
        public long? DocumentoPadreId { get; set; }
        public string TipoAdjunto { get; set; }
        public string NResolucionPolitica { get; set; }
        public string NResolucionProcedimiento { get; set; }
        public string Materia { get; set; }
        public bool? ApruebaAlcanceGradualSEV { get; set; }
        public bool RevisionPoliticaAmbiental { get; set; }
        public bool? DetActDeConcientizacion { get; set; }
        public bool? RevisionProcBienesMuebles { get; set; }
        public bool? ApruebaDiagnostico { get; set; }
        public bool? PuestaEnMarchaCEV { get; set; }
        public bool? DefinePropuestaConcientizados { get; set; }
        public bool FormatoDocumento { get; set; }
        public bool Donacion { get; set; }
        public bool Destruccion { get; set; }
        public bool Reparacion { get; set; }
        public long? ServicioId { get; set; }
        public float? TotalColaboradoresConcientizados { get; set; }
        public int? TotalColaboradoresCapacitados { get; set; }
        public int? NComprasRubros { get; set; }
        public int? NComprasRubros2 { get; set; }
        public int? NComprasRubrosFuera { get; set; }
        public int? NComprasCriterios { get; set; }
        public int? NComprasCriterios2 { get; set; }
        public int? NComprasCriteriosFuera { get; set; }
        public string FuncionariosDesignados { get; set; }
        public int FuncionariosDesignadosNum { get; set; }
        public int? EtapaSEV_docs { get; set; }
        public bool? ElaboraPolitica { get; set; }
        public bool? ActualizaPolitica { get; set; }
        public bool? MantienePolitica { get; set; }
        public bool? E1O1RT2 { get; set; }
        public bool? DefinicionesEstrategicas { get; set; }
        public bool? Consultiva { get; set; }
        public float? PorcentajeConcientizadosEtapa2 { get; set; }
        public float? PropuestaPorcentaje { get; set; }
        public float? PorcentajeCapacitadosEtapa2 { get; set; }
        public bool? GestionEnergia { get; set; }
        public bool? GestionVehiculosTs { get; set; }
        public bool? TrasladoSustentable { get; set; }
        public bool? GestionAgua { get; set; }
        public bool? MateriaGestionPapel { get; set; }
        public bool? GestionResiduosEc { get; set; }
        public bool? GestionComprasS { get; set; }
        public bool? GestionBajaBs { get; set; }
        public bool? HuellaC { get; set; }
        public bool? CambioClimatico { get; set; }
        public bool? OtraMateria { get; set; }
        public bool? ConsultaPersonal { get; set; }
        public string DefinePolitica { get; set; }
        public bool? ActividadesCI { get; set; }
        public string PropuestaTemasCI { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public Servicio Servicio { get; set; }
    }
}
