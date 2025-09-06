using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Families.Model
{
    public class DismissFamilyConfig
    {
        /// <summary>
        /// 付款RMB
        /// </summary>
        public decimal PayRMB { get; set; }

        /// <summary>
        /// 有效期(小时)
        /// </summary>
        public double Validate { get; set; }
    }
}
