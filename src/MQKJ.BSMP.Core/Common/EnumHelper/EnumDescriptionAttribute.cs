using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EnumHelper
{
    [AttributeUsage(AttributeTargets.Field,AllowMultiple = false)]
    public sealed class EnumDescriptionAttribute:Attribute
    {
        private string description;
        public string Description { get { return description; } }

        public EnumDescriptionAttribute(string description)
           : base()
        {
            this.description = description;
        }
    }
}
