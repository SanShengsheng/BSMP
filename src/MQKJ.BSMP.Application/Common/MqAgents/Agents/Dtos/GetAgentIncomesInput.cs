using Abp.Runtime.Validation;
using MQKJ.BSMP.Common.IncomeRecords;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.MqAgents.Agents.Dtos
{
    public class GetAgentIncomesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public string AgentName { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        public string UpAgentName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Phone { get; set; }
        public string Company { get; set; }

        private string _sortType { get; set; }
        public string SortType
        {
            get => string.IsNullOrEmpty(_sortType) ? "DESC" : _sortType;
            set => _sortType = value;
        }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "TotalRunWaterCount";
            }
        }
    }

    public enum AgentDataOrder
    {
        [EnumHelper.EnumDescription("TotalOrderCount")]
        TotalOrderCount = 1,

        [EnumHelper.EnumDescription("TotalRunWaterCount")]
        TotalRunWaterCount = 2,

        [EnumHelper.EnumDescription("TotalIncomeCount")]
        TotalIncomeCount = 3,

        [EnumHelper.EnumDescription("WithDrawedMoney")]
        WithDrawedMoney = 4,

        [EnumHelper.EnumDescription("CanWithDrawedMoney")]
        CanWithDrawedMoney = 5,
    }

    public enum OrderType
    {
        [EnumHelper.EnumDescription("ASC")]
        AscendingOrder = 1,

        [EnumHelper.EnumDescription("DESC")]
        DescendingOrder = 2
    }
}
