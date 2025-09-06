using MQKJ.BSMP.BigRisks.WeChat.WechatPay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.EnergyRecharges.Dtos
{
    public class BuyEnergyInput
    {
        /// <summary>
        /// 精力充值表Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 家庭Id
        /// </summary>
        public int FamilyId { get; set; }

        public int BabyId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid PlayerId { get; set; }

    }
}
