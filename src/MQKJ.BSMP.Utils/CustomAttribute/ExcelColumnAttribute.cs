using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Utils.CustomAttribute
{
    public class ExcelColumnAttribute:Attribute
    {
        public string ColumnName { get; set; }

        public ExcelColumnAttribute(string name)
        {
            ColumnName = name;
        }
    }
}
