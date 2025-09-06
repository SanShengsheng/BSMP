

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class CoinRechargeListDto : ISearchOutModel<CoinRecharge,int> 
    {
        public int Id { get; set; }

        /// <summary>
        /// MoneyAmount
        /// </summary>
        public decimal MoneyAmount { get; set; }

        public RechargeLevel RechargeLevel { get; set; }



        /// <summary>
        /// CoinCount
        /// </summary>
        public int CoinCount { get; set; }

        /// <summary>
        /// ÊÇ·ñÊ×³å
        /// </summary>
        public bool IsFirstCharge { get; set; }




    }
}