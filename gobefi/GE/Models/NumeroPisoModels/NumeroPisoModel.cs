using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.NumeroPisoModels
{
    public class NumeroPisoModel
    {
        [JsonProperty(PropertyName = "value")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Nombre { get; set; }
        [JsonProperty(PropertyName = "numPiso")]
        public int Numero { get; set; }
        [JsonProperty(PropertyName = "nivel")]
        public int Nivel { get; set; }
        public bool Active { get; set; }
    }
}
