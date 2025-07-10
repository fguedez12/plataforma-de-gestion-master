using GobEfi.Web.Core.Metadata;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.DivisionModels
{
    [ModelMetadataType(typeof(DivisionMetadata))]
    public class DivisionBaseModel : BaseModel<long>
    {
    }
}
