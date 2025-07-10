using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using GobEfi.Web.Models.CompraMedidorModels;
using Microsoft.AspNetCore.Http;

namespace GobEfi.Web.Models.CompraModels
{
    public class CompraForEdit
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public long EnergeticoId { get; set; }
        public string FechaCompra { get; set; }
        public string InicioLectura { get;set; }
        public string FinLectura { get;set; }
        public long? NumeroClienteId { get;set; }
        public Collection<CompraMedidorForEdit> ListaMedidores { get;set; }
        public double Costo { get;set;}
        public double Consumo { get; set; }
        public double ConsumoCompra { get; set; }
        public string TextUnidadMedida { get; set; }
        public long FacturaId { get; set; }
        public IFormFile Factura { get; set; }
        public long DivisionId { get; set; }
        public string Observacion { get; set; }
        //public string CreadoEn { get;set; }
        //public string ActualizadoEn { get; set; }

        public string ObservacionRevision { get; set; }
        public long? UnidadMedidaId { get; set; }
        public string EstadoValidacion { get; set; }

        public long CreatedByDivisionId { get; set; }
        public bool SinMedidor { get; set; }

    }
}