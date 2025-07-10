using GobEfi.Web.Models.EmpresaDistribuidoraModels;
using GobEfi.Web.Models.MedidorModels;
using GobEfi.Web.Models.NumeroClienteModels;
using GobEfi.Web.Models.TipoTarifaModels;
using System.Collections.Generic;

namespace GobEfi.Web.Models.EnergeticoModels
{
    public class EnergeticoConfigModel
    {
        public EnergeticoConfigModel()
        {
            this.Clientes = new List<NumeroClienteModel>();
            this.Medidores = new List<MedidorModel>();
            this.EmpresaDistribuidora = new List<EmpresaDistribuidoraModel>();
            this.TipoTarifa = new List<TipoTarifaModel>();
        }

        public string Icono { get; set; }
        public string Nombre { get; set; }


        public ICollection<NumeroClienteModel> Clientes;
        public ICollection<MedidorModel> Medidores;
        public List<EmpresaDistribuidoraModel> EmpresaDistribuidora;
        public List<TipoTarifaModel> TipoTarifa;
    }
}
