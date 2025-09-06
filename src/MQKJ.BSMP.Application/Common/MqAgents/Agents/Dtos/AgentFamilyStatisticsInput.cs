using Abp.Runtime.Validation;
using MQKJ.BSMP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.MqAgents.Agents.Dtos
{
    public class AgentFamilyStatisticsInput: PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        public AgentFamilyOrderWay OrderWay { get; set; }
        private string _sortType { get; set; }
        public string SortType
        {
            get => string.IsNullOrEmpty(_sortType) ? "DESC" : _sortType;
            set => _sortType = value;
        }

        public void Normalize()
        {
            if (Sorting == "CreationTime desc")
            {
                Sorting = "NewCreateFamilyCount";
            }
        }
    }

    public enum AgentFamilyOrderWay
    {
        [EnumHelper.EnumDescription("NewCreateFamilyCount")]
        Default = 0,

        [EnumHelper.EnumDescription("NewCreateFamilyCount")]
        NewCreateFamilyWay = 1,

        [EnumHelper.EnumDescription("NewRechargeFamilyCount")]
        NewRechargeFamilyWay = 2,

        [EnumHelper.EnumDescription("RechargeFamilyCount")]
        FamilyRechargeWay = 3,

        [EnumHelper.EnumDescription("RepeatRechargeFamilyCount")]
        RepeatRechargeFamily = 4,

        [EnumHelper.EnumDescription("NewFamilyWaterRunCount")]
        NewFamilyWaterRunWay = 5,

        [EnumHelper.EnumDescription("FamilyWaterRunCount")]
        FamilyWaterRunWay = 6,

        [EnumHelper.EnumDescription("RechargeInversionRate")]
        RechargeInversionRateWay = 7,

        [EnumHelper.EnumDescription("PerCustomerTransaction")]
        PerCustomerTransactionWay = 8
    }
}
