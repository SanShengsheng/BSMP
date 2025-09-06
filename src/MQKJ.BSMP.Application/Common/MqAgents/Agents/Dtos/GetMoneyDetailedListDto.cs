using Abp.AutoMapper;
using MQKJ.BSMP.Common.IncomeRecords;
using MQKJ.BSMP.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.MqAgents.Dtos
{
    [AutoMapFrom(typeof(IncomeRecord))]
    public class GetMoneyDetailedListDto
    {
        /// <summary>
        /// 收入
        /// </summary>
        public double Income { get; set; }

        /// <summary>
        /// 实际收入
        /// </summary>
        public double RealIncome { get; set; }


        /// <summary>
        ///当前收益比例
        /// </summary>
        public double CurrentEarningRatio { get; set; }

        /// <summary>
        /// 收益人
        /// </summary>
        public MqAgentDto MqAgent { get; set; }

        public MqAgentDto SecondAgent { get; set; }

        /// <summary>
        /// 提现状态
        /// </summary>
        public WithdrawMoneyState WithdrawMoneyState { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public DateTime CreationTime { get; set; }
    }

    [AutoMapFrom(typeof(MqAgent))]
    public class MqAgentDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        //[Required]
        public string UserName { get; set; }

        public AgentLevel Level { get; set; }

        public string HeadUrl { get; set; }

        public string NickName { get; set; }

        public MqAgentDto UpperLevelMqAgent { get; set; }

        //public string NickName { get; set; }
    }
}
