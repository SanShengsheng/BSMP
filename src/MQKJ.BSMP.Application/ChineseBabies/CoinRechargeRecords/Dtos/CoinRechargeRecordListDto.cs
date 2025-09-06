

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Orders;
using Abp.AutoMapper;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    [AutoMapFrom(typeof(CoinRechargeRecord))]
    public class CoinRechargeRecordListDto : ISearchOutModel<CoinRechargeRecord,Guid> 
    {

        
		/// <summary>
		/// RechargeCount
		/// </summary>
		public int RechargeCount { get; set; }



		/// <summary>
		/// RechargerId
		/// </summary>
		public Guid RechargerId { get; set; }

        public Player Recharger { get; set; }



        /// <summary>
        /// FamilyId
        /// </summary>
        public int FamilyId { get; set; }

        public Family Family { get; set; }



		/// <summary>
		/// OrderId
		/// </summary>
		public Guid? OrderId { get; set; }



		/// <summary>
		/// SourceType
		/// </summary>
		public SourceType SourceType { get; set; }




    }
}