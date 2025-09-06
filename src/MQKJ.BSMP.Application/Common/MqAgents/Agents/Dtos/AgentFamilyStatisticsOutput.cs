using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.MqAgents.Agents.Dtos
{
    public class AgentFamilyStatisticsOutput
    {
        public int AgentId { get; set; }

        public string NickName { get; set; }
        public string UserName { get; set; }
        public string HeadUrl { get; set; }
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 时间段内创建家庭数量
        /// </summary>
        public int NewCreateFamilyCount { get; set; }

        /// <summary>
        /// 时间段内的家庭在时间段内充值的家庭数量
        /// </summary>
        public int NewRechargeFamilyCount { get; set; }

        /// <summary>
        /// 时间段外的家庭 在时间段内充值的家庭数量
        /// </summary>
        public int RechargeFamilyCount { get; set; }

        /// <summary>
        /// 时间段外创建家庭数量
        /// </summary>
        public int CreateFamilyCount { get; set; }

        /// <summary>
        /// 重复充值的家庭数量
        /// </summary>
        public int RepeatRechargeFamilyCount { get; set; }

        /// <summary>
        /// 时间段内的家庭在时间段内产生的流水
        /// </summary>
        public double NewFamilyWaterRunCount { get; set; }
        
        /// <summary>
        /// 时间段外的家庭在时间段内产生的流水
        /// </summary>
        public double FamilyWaterRunCount { get; set; }

        /// <summary>
        /// 充值转化率(时间段内充值家庭数量/时间段内家庭数量)
        /// </summary>
        public double RechargeInversionRate { get; set; }

        /// <summary>
        /// 客单价(时间段内家庭流水/时间段内充值家庭数量)
        /// </summary>
        public double PerCustomerTransaction { get; set; }
    }
}
