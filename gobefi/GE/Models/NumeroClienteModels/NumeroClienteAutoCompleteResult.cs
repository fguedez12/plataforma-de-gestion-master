using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.NumeroClienteModels
{
    public class NumeroClienteAutoCompleteResult
    {
        public bool Success { get; set; }
        public string Text { get; set; }
        public NumeroClienteModel Data { get; set; }
    }
}
