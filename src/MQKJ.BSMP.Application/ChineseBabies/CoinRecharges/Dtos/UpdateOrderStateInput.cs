using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos
{
    public class UpdateOrderStateInput
    {
        public int Id { get; set; }

        /// <summary>
        ///     微信支付订单号（二选一）
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        ///     商户系统的订单号，与请求一致（二选一）
        /// </summary>
        public string OutTradeNo { get; set; }

        public int FamilyId { get; set; }

        //public int? BabyId { get; set; }

        public Guid? PlayerId { get; set; }

        /// <summary>
        /// 大礼包Id
        /// </summary>
        public Guid? PropBagId { get; set; }

        public Guid? OrderId { get; set; }

        /// <summary>
        /// 是否是虚拟充值
        /// </summary>
        public bool isVirtualRecharge { get; set; }
    }
}
