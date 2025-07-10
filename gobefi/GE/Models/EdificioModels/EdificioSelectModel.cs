using GobEfi.Web.Core.Metadata;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EdificioModels
{
    public class EdificioSelectModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
    }
}
