using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.MuroModels
{
    public class MuroPasoDosModel
    {
        public long Id { get; set; }
        public float Bearing { get; set; }
        public string Orientation { get; set; }
        public float Distance { get; set; }
        public string Tipo { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public int Order { get; set; }
    }
}
