using GobEfi.Web.Models.ArchivoAdjuntoModels;
using GobEfi.Web.Models.CompraModels;
using GobEfi.Web.Models.EnergeticoModels;
using GobEfi.Web.Models.MedidorModels;
using GobEfi.Web.Models.NumeroClienteModels;
using System.Collections.Generic;


namespace GobEfi.Web.Models.DivisionModels
{
    public class DivisionDataModel
    {
        public ICollection<NumeroClienteDataModel> NumerosClientes { get; set; }
        public ICollection<MedidorDataModel> Medidores { get; set; }
        public ICollection<EnergeticoDataModel> Energeticos { get; set; }
        
    }
}
