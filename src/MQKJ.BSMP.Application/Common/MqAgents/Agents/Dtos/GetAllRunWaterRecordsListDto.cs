using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Common.IncomeRecords;
using MQKJ.BSMP.Orders;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.MqAgents.Agents.Dtos
{
    [AutoMapFrom(typeof(IncomeRecord))]
    public class GetAllRunWaterRecordsListDto
    {
        public PagedResultDto<GetAllRunWaterRecordsListDtoWaterRecordModl> WaterRecords { get; set; }
        /// <summary>
        /// 经纪/主播公司总收入
        /// </summary>
        public double? BrokerTotalIncome { get; set; }
        public double? RoyaltyRate { get; internal set; }

        /// <summary>
        /// 总充值
        /// </summary>
        public double TotalPayment { get; set; }
        /// <summary>
        /// 主播总收益
        /// </summary>
        public double TotalAnchorIncome { get; set; }

    }
    public class GetAllRunWaterRecordsListDtoWaterRecordModl
    {
        /// <summary>
        /// 业务人Id
        /// </summary>
        public int MqAgentId { get; set; }

        /// <summary>
        /// 收益人Id
        /// </summary>
        public int? SecondAgentId { get; set; }

        public DateTime CreationTime { get; set; }
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

        public IncomeTypeEnum IncomeTypeEnum { get; set; }

        public IncomeRecordMqAgentDto SecondAgent { get; set; }


        public IncomeRecordMqAgentDto MqAgent { get; set; }

        public Order Order { get; set; }

    }
    [AutoMapFrom(typeof(MqAgent))]
    public class IncomeRecordMqAgentDto
    {
        public string UserName { get; set; }

        public string NickName { get; set; }

        public AgentLevel Level { get; set; }
        public IncomeRecordMqAgentDto UpperLevelMqAgent { get; set; }
    }

    [AutoMapFrom(typeof(Order))]
    public class IncomeRecordOrderDto
    {
        public Guid PlayerId { get; set; }
        public string OrderNumber { get; set; }

        public IncomeRecordPlayerDto Player { get; set; }

        public int FamilyId { get; set; }
        public IncomeRecordFamilyDto Family { get; set; }
    }

    [AutoMapFrom(typeof(Player))]
    public class IncomeRecordPlayerDto
    {
        public string NickName { get; set; }
    }

    [AutoMapFrom(typeof(Family))]
    public class IncomeRecordFamilyDto
    {
        public Guid FatherId { get; set; }
        public Guid MotherId { get; set; }
    }
}
