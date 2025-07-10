using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Services.Helpers
{
    public class CustomValidations
    {
    }
    public class MinimunList : ValidationAttribute
    {
        private readonly int minElement;

        public MinimunList(int minElement)
        {
            this.minElement = minElement;
        }

        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
            {
                return list.Count >= minElement;
            }
            return false;
        }
    }
}
