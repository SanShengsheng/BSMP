using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.EnterprisePayments.Dtos
{
    public class RequestWithdrawMoneyInput
    {
        public int AgentId { get; set; }

        public double Amount { get; set; }

        /// <summary>
        /// 申请支付平台，用户提现申请请求使用哪一个平台
        /// </summary>
        public WithdrawMoneyType RequestPlatform { get; set; }
    }
}
