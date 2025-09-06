
using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Orders;
using MQKJ.BSMP.Players;

namespace  MQKJ.BSMP.ChineseBabies.Dtos
{
    [AutoMapTo(typeof(CoinRechargeRecord))]
    public class CoinRechargeRecordEditDto:IAddModel<CoinRechargeRecord,Guid>,IEditModel<CoinRechargeRecord,Guid>
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// RechargeCount
		/// </summary>
		public int RechargeCount { get; set; }



		/// <summary>
		/// RechargerId
		/// </summary>
		public Guid? RechargerId { get; set; }

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