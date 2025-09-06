using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.LoveCard.LoveCards.Dtos
{
    public class GetUnLockResultInput
    {
        public Guid PlayerId { get; set; }

        /// <summary>
        ///     微信支付订单号（二选一）
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        ///     商户系统的订单号，与请求一致（二选一）
        /// </summary>
        public string OutTradeNo { get; set; }
    }
}
