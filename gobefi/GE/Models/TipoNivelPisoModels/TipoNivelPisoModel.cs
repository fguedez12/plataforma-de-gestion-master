using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.TipoNivelPisoModels
{
    public class TipoNivelPisoModel
    {
        [JsonProperty(PropertyName = "value")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Nombre { get; set; }
    }
}
