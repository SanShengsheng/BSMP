using MQKJ.BSMP.BigRisks.WeChat.WechatPay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.UnLocks.Dtos
{
    public class UnlockWeChatAccountInput
    {
        /// <summary>
        /// 解锁人的Id
        /// </summary>
        //public Guid UnLockerId { get; set; }

        /// <summary>
        /// 被解鎖人的Id
        /// </summary>
        //public Guid BeUnLockerId { get; set; }


        /// <summary>
        /// 玩家Id
        /// </summary>
        public Guid PlayerId { get; set; }

        public int ProductId { get; set; }

        /// <summary>
        /// 客户端类型
        /// </summary>
        public ClientType ClientType { get; set; }

        public string Body { get; set; } = "默奇网络科技有限公司";

        public string Attach { get; set; } = "微信支付信息";

        public decimal Totalfee { get; set; }

        public const string NotifyUlr = "";
    }
}
