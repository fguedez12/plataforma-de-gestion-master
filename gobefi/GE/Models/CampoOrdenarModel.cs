using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models
{
    public class CampoOrdenarModel
    {
        public CampoOrdenarModel(string title, string value)
        {
            this.Title = title;
            this.Value = value;
        }

        public string Title { get; set; }
        public string Value { get; set; }
    }
}
