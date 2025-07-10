using System;
using System.Collections.Generic;
using System.Text;

namespace GobEfi.FV.Shared.Entities
{
    public class ModeloEm : IId
    {
        public long Id { get; set; }
        public long IdEm { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Traccion { get; set; }
        public string Transmision { get; set; }
        public string Combustible { get; set; }
        public string Propulsion { get; set; }
        public string Cilindrada { get; set; }
        public string Carroceria { get; set; }
        public string Codigo_informe_tecnico { get; set; }
        public string Fecha_homologacion { get; set; }
        public string Categoria_vehiculo { get; set; }
        public string Empresa_homologacion { get; set; }
        public string Norma_emisiones { get; set; }
        public string Co2 { get; set; }
        public string Rendimiento_ciudad { get; set; }
        public string Rendimiento_carretera { get; set; }
        public string Rendimiento_mixto { get; set; }
        public string Rendimiento_puro_electrico { get; set; }
        public string Rendimiento_enchufable_combustible { get; set; }
        public string Rendimiento_enchufable_electrico { get; set; }
        public string Tipo_de_conector_ac { get; set; }
        public string Tipo_de_conector_dc { get; set; }
        public string Acumulacion_energia_bateria { get; set; }
        public string Capacidad_convertidor_vehiculo_electrico { get; set; }
        public string Autonomia { get; set; }
        public string Rendimiento { get; set; }
        public string Created_at { get; set; }
        public string Updated_at { get; set; }
        public string Img { get; set; }
        public string Link { get; set; }
        public int Eliminar { get; set; }
        public List<Vehiculo> Vehiculos { get; set; }
    }
}
