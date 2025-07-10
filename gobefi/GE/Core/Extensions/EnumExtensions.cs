using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string ToDescription(this System.Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public static string _toString(this object value)
        {
            if (value == null)
                return "";
            return value.ToString();
        }

        public static long _toLong(this object value)
        {
            long ret = -1;

            if (!long.TryParse(value._toString(), out ret))
                return -1;

            return ret;
        }
    }
}
