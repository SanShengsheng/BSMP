using MQKJ.BSMP.Common;
using System;

namespace MQKJ.BSMP.Pay
{
    public class PostOrderAsyncInput
    {
        /// <summary>
        /// 提现申请/记录编号
        /// </summary>
        public Guid ApplyId { get; set; }
        /// <summary>
        /// 请求平台
        /// </summary>
        public WithdrawMoneyType? RequestPlatform { get; set; }
       }
}