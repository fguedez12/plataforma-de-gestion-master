using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EdificioModels;
using GobEfi.Web.Models.ServicioModels;
using GobEfi.Web.Models.TipoUsoModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.DivisionModels
{
    public class DivisionDeleteModel
    {
        public long Id { get; set; }
        [DisplayName("Edificio")]
        public EdificioModel Edificio { get; set; }
        [DisplayName("Servicio")]
        public ServicioModel Servicio { get; set; }
        [DisplayName("Nombre Unidad")]
        public string NombreUnidad { get; set; }
        [DisplayName("Tipo de Uso")]
        public TipoUsoModel TipoDeUso { get; set; }
        public string Pisos { get; set; }
        [DisplayName("Superficie (m²)")]
        public string Superficie { get; set; }
        [DisplayName("Tipo de Propiedad")]
        public TipoPropiedad TipoPropiedad { get; set; }
        [DisplayName("N° de Rol")]
        public string NroRol { get; set; }
        [DisplayName("Año de Construcción")]
        public int AnioConstruccion { get; set; }
        [DisplayName("N° de Funcionarios")]
        public int NroFuncionarios { get; set; }
    }
}
