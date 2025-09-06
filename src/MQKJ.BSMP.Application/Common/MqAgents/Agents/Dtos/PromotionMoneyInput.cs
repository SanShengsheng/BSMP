using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.MqAgents.Dtos
{
    public class PromotionMoneyInput
    {
        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal Amount { get; set; }

        public Guid? PlayerId { get; set; }

        public bool IsTest { get; set; } 

        public string UserName { get; set; }

        public string Spbill_Create_IP { get; set; }


        public int AgentId { get; set; }
    }
}
