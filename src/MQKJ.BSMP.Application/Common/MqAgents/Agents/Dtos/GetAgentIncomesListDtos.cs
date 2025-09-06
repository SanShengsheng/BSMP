using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.MqAgents.Agents.Dtos
{
    public class GetAgentIncomesListDtos
    {
        public int AgentId { get; set; }

        public string NickName { get; set; }

        public string UserName { get; set; }

        public string HeadeUrl { get; set; }

        public AgentLevel AgentLevel { get; set; }

        /// <summary>
        /// 上级代理名字
        /// </summary>
        public string UpAgentName { get; set; }

        //public int? UpAgentId { get; set; }

        /// <summary>
        /// 总订单
        /// </summary>
        public int TotalOrderCount { get; set; }

        /// <summary>
        /// 总流水
        /// </summary>
        public double TotalRunWaterCount { get; set; }

        /// <summary>
        /// 总收益
        /// </summary>
        public double TotalIncomeCount { get; set; }

        /// <summary>
        /// 已提现钱
        /// </summary>
        public double WithDrawedMoney { get; set; }

        /// <summary>
        /// 可提现钱
        /// </summary>
        public double CanWithDrawedMoney { get; set; }



        public double FirstRunWaterCount { get; set; }

        public double SecondRunWaterCount { get; set; }

        public string PhoneNumber { get; set; }

        public double TotalSubsidyAmount { get; set; }

    }
}
