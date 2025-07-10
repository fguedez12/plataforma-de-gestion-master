using GobEfi.Web.Models.CompraMedidorModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace GobEfi.Web.Models.CompraModels
{
    public class CompraForRegister
    {
        public long Id { get; set; }
        public double? ConsumoCompra { get; set; }
        public double? Costo { get; set; }
        public string Observacion { get; set; }

        //[Range(typeof(DateTime), "1/1/2000", "31/1/9999", ErrorMessage="Fecha Inicio de Lecura fuera de Rango")]
        //public DateTime? InicioLectura { get; set; }
        public string InicioLectura { get; set; }

        //[Range(typeof(DateTime), "1/1/2000", "31/1/9999", ErrorMessage="Fecha Fin de Lectura fuera de Rango")]
        //public DateTime? FinLectura { get; set; }
        public string FinLectura { get; set; }

        //[Range(typeof(DateTime), "1/1/2000", "31/1/9999", ErrorMessage="Fecha de Compra fuera de Rango")]
        //public DateTime? FechaCompra { get; set; }
        public string FechaCompra { get; set; }


        public Collection<CompraMedidorForRegister> ListaMedidores { get; set; }
        public long? NumeroClienteId { get; set; }
        public long? MedidorId { get; set; }
        public long DivisionId { get; set; }
        public long? EnergeticoId { get; set; }
        public long? FacturaId { get; set; }
        public long? UnidadMedidaId { get; set; }
        public bool SinMedidor { get; set; }
    }
}
