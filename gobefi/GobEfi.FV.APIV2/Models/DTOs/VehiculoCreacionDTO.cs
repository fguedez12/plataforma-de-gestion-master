using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.APIV2.Models.DTOs
{
    public class VehiculoCreacionDTO
    {
        public string Patente { get; set; }
        public string Nombre { get; set; }
        public int Anio { get; set; }
        public long ModeloId { get; set; }
        public long TipoPropiedadId { get; set; }
        public string TipoVehiculo { get; set; }
        public string Marca { get; set; }
        public string Propulsion { get; set; }
        public long MinisterioId { get; set; }
        public long ServicioId { get; set; }
        public int Kilometraje { get; set; }
        public int ModeloIdApi { get; set; }
        public List<ImagenDTO> Imagenes { get; set; }
    }
}
