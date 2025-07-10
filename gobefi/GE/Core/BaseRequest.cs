using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core
{
    public class BaseRequest
    {
        public string userId { get; set; }
        public int Page { set; get; } = 1;
        public int Size { set; get; } = 20;
        public string FieldName { set; get; }
        public bool Direction { set; get; }
    }
}
