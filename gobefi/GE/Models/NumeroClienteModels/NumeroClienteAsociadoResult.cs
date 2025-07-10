using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.NumeroClienteModels
{
    public class NumeroClienteAsociadoResult
    {

        /*
         success = true, responseText = "Asociacion realizada con exito.", data = model.Id
         */

        public bool Success { get; set; }
        public string ResponseText { get; set; }
        public long Data { get; set; }
    }
}
