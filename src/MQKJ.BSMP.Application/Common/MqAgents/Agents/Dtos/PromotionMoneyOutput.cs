using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.MqAgents.Dtos
{
    public class PromotionMoneyOutput
    {
        /// <summary>
        /// 是否提现成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrDescription { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public string ErrCode { get; set; }
    }
}
