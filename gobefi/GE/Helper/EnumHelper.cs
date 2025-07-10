using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GobEfi.Web.Helper
{
    public class EnumHelper
    {
        public static string GetEnumDisplayName(Enum enumVal)
        {
            MemberInfo[] memInfo = enumVal.GetType().GetMember(enumVal.ToString());
            DisplayAttribute attribute = CustomAttributeExtensions.GetCustomAttribute<DisplayAttribute>(memInfo[0]);
            return attribute.Name;
        }
    }
}
