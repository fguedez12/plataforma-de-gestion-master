using GobEfi.Web.Models.NumeroClienteModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.MedidorModels
{
    public class MedidorModel : BaseModel<long>
    {
        public bool _inteligente { get; set; }
        public bool _compartido { get; set; }
        public bool _factura { get; set; }


        [Required(ErrorMessage = "Numero de Medidor es obligatiorio")]
        [DisplayName("Número de Medidor:")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Numero de Cliente es obligatorio")]
        [DisplayName("Número de Cliente:")]
        public long NumeroClienteId { get; set; }
        
        public int NumFases { get; set; }
        public string Medicion { get; set; }
        public int FlujoMedicion { get; set; }
        public long DivisionId { get; set; }
        public string DivisionNombre { get; set; }
        public string ServicioNombre { get; set; }
        public string InstitucionNombre { get; set; }

        public NumeroClienteModel NumeroCliente { get; set; }


        public ICollection<MedidorParaAsociar> Medidores { get; set; }

        public string Compartido
        {
            get
            {
                return _compartido ? "Si" : "No";
            }
        }

        public string Inteligente
        {
            get
            {
                return _inteligente ? "Si" : "No";
            }
        }

        public string Factura
        {
            get
            {
                return _factura ? "Si" : "No";
            }
        }
    }
}
