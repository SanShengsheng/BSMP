using Abp.Application.Services.Dto;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.Common.MqAgents.Agents.Dtos;
using MQKJ.BSMP.MqAgents.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MQKJ.BSMP.Web.Areas.ChineseBabies.Models
{
    public class RunWaterRecordModel
    {
        public List<CoinRechargeListDto> CoinRechargeListDtos { get; set; }

        public StaticPagedList<GetAllRunWaterRecordsListDtoWaterRecordModl> MoneyDetailedListDtos { get; set; }
        public double? BrokerTotalIncome { get; internal set; }
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
}
