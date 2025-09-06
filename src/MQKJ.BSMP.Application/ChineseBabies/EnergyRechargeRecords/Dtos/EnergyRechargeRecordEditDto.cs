
using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Players;

namespace  MQKJ.BSMP.ChineseBabies.Dtos
{
    [AutoMapTo(typeof(EnergyRechargeRecord))]
    public class EnergyRechargeRecordEditDto:IAddModel<EnergyRechargeRecord,Guid>, IEditModel<EnergyRechargeRecord, Guid>
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// CoinAmount
		/// </summary>
		public int CoinAmount { get; set; }



		/// <summary>
		/// EnergyCount
		/// </summary>
		public int EnergyCount { get; set; }



		/// <summary>
		/// RechargerId
		/// </summary>
		public Guid? RechargerId { get; set; }



		/// <summary>
		/// Recharger
		/// </summary>
		public Player Recharger { get; set; }



		/// <summary>
		/// BabyId
		/// </summary>
		public int BabyId { get; set; }



		/// <summary>
		/// Baby
		/// </summary>
		public Baby Baby { get; set; }



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