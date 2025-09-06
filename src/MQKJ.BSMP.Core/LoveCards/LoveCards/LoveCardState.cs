using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.LoveCards
{
    public enum LoveCardState
    {
        /// <summary>
        /// 未知
        /// </summary>
        [EnumHelper.EnumDescription("未知")]
        UnKonw = 0,

        /// <summary>
        /// 未审核
        /// </summary>
        [EnumHelper.EnumDescription("未审核")]
        UnAudited = 1,

        /// <summary>
        /// 审核通过
        /// </summary>
        [EnumHelper.EnumDescription("已审核")]
        Audited = 2
    }
}
