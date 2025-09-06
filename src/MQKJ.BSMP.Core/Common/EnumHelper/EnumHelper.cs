using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EnumHelper
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentException("value");
            }
            string description = value.ToString();
            var fieldInfo = value.GetType().GetField(description);
            if (fieldInfo == null)
            {
                return description;
            }
            var attributes =
                (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            return description;
        }
    }
}
