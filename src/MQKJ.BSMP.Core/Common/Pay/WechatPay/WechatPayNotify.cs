using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.WechatPay
{
    public class WechatPayNotify : FullAuditedEntity<Guid>
    {
        public string OutTradeNo { get; set; }
        public string TransactionId { get; set; }
        public string NotifyData { get; set; }
        public string ErrorMessage { get; set; }
    }
}
