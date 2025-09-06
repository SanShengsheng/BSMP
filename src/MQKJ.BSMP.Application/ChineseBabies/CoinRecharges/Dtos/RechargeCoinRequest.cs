using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos
{
    public class RechargeCoinRequest
    {
        public int RechargeId { get; set; }
        public int FamilyId { get; set; }
        public Guid PlayerId { get; set; }
        /// <summary>
        /// 是否虚拟充值
        /// </summary>
        public bool IsVirtual { get; set; }

        public Guid? OrderId { get; set; }
    }
}
