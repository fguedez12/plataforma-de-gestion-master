using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.DTOs.UnidadesDTO
{
    public class UnidadReporteConsumoDTO
    {
        public long Id { get; set; }
        public string Direccion { get; set; }
        public int AccesoFactura { get; set; }
        public long ServicioResponsableId { get; set; }
        public bool? Compromiso2022 { get; set; }
        public int? EstadoCompromiso2022 { get; set; }
        public string Justificacion { get; set; }
        public string ObservacionCompromiso2022 { get; set; }
        public string EstadoJustificacion 
        { get 
            {
                switch (EstadoCompromiso2022)
                {
                    case null:
                        return "Sin justificar";
                    case 0:
                        return "Ingresado";
                    case 1:
                        return "Ingresado";
                    case 2:
                        return "Observado";
                    case 3:
                        return "Validado";
                    default:
                        return "Sin justificar";
                }
                //return (this.Compromiso2022 == null ? "Sin justificar" : "Ok" ); 
            } 
        }
    }
}
