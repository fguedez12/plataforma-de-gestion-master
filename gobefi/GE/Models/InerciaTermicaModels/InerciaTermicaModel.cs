using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.InerciaTermicaModels
{
    public class InerciaTermicaModel : BaseModel<long>
    {
        [JsonProperty(PropertyName = "value")]
        public long Valor { get { return Id; } }
        [JsonProperty(PropertyName = "name")]
        public string Nombre { get; set; }
    }
}
