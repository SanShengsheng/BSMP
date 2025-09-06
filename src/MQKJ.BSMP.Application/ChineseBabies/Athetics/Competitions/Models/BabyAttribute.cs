using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Models
{
    public class BabyAttribute
    {
        /// <summary>
        /// 宝宝属编码
        /// </summary>
        public BabyAttributeCode BabyAttributeCode { get; set; }

        /// <summary>
        /// 宝宝属性出现的次数
        /// </summary>
        public int Count { get; set; }
    }
}
