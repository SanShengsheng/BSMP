
using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;

namespace  MQKJ.BSMP.ChineseBabies.Dtos
{
    [AutoMapTo(typeof(CoinRecharge))]
    public class CoinRechargeEditDto:IEditModel<CoinRecharge,int>,IAddModel<CoinRecharge,int>
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
		/// <summary>
		/// MoneyAmount
		/// </summary>
		public decimal MoneyAmount { get; set; }



		/// <summary>
		/// CoinCount
		/// </summary>
		public int CoinCount { get; set; }




    }
}